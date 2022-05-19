using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net.Http;

namespace WebAPI.Controllers
{
    //the contoller is a service (api)
    [ApiController]
    [Route("/api/[controller]/")]

    public class ContactsController : Controller
    {
        private readonly IContactService _service;
        public ContactsController(IContactService service)
        {
            _service = service;
        }

        // Get all contacts of the current user
        
        [HttpGet]
        public IActionResult Index()
        {
            string user = HttpContext.Session.GetString("userName");
            /*
            if (HttpContext.Session.GetString("userName") == null)
            {
                return BadRequest("User MUST Login!");
            }
            */
            return Ok(_service.GetAllContacts("david"));
        }

        // Get a certain contact according to his id 

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            string user = HttpContext.Session.GetString("userName");
            /*
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            */
            if (_service.CheckContactByID(id, "david")){
                return Ok(_service.Get(id, "david"));
            } else
               return NotFound();
        }

        // Get all messages from a certain contact (according to the id contact)
        [HttpGet("{id}/messages")]
        public IActionResult DetailsMessages(string id)
        {
            string user = HttpContext.Session.GetString("userName");
            /*
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            */
            if (_service.CheckContactByID(id, "david"))
            {
                return Ok(_service.Get(id, "david").Messages);
            }
            else
                return NotFound();
        }


        // add a new contact to the current user
        [HttpPost]

        public IActionResult CreateContact([Bind("Id,Name,Server")] Contact contact)
        {
            /*
            if (HttpContext.Session.GetString("userName") == null)
            {
                return BadRequest("User MUST Login!");
            }
            */
            _service.Create(contact.Id, contact.Name, contact.Server, "david");
            return Created(string.Format("/api/Contacts/{0}", contact.Id), contact);
        }

        // add a new message to a certain contact
        [HttpPost("{id}/messages")]
        public IActionResult CreateMessage([Bind("Content")] Message message, string id)
        {
            string user = HttpContext.Session.GetString("userName");
            /*
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }*/
            // check if the id contact exist
            if (!_service.CheckContactByID(id, "david"))
            {
                return NotFound();
            }
            
            message.Id = _service.GetNextIdMessage(id, "david");
            message.Created = DateTime.Now;
            message.Sent = true;
            _service.GetMessages(id, "david").Add(message);
            // change the info of the conatct
            _service.Get(id, "david").Last = message.Content;
            _service.Get(id, "david").LastDate = DateTime.Now;

            return Created(string.Format("/api/Contacts/{0}/messages", message.Id), message);
        }

        [HttpPut("{id}")]
        //change a cerain contact info (only name and server)
        public IActionResult Put([Bind("Name,Server")] Contact contact, string id)
        {
            /*
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }*/
            if (_service.CheckContactByID(id, "david"))
            {
                _service.Get(id, "david").Name = contact.Name;
                _service.Get(id, "david").Server = contact.Server;
                return NoContent();
            }
            else
                return NotFound();
        }
        // delete a certain contact 
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            /*
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            */
            if (_service.CheckContactByID(id, "david"))
            {
                _service.Delete(id, "david");
                return NoContent();
            }
            else
                return NotFound();
        }
        // get a certain message(id2) from certain contact(id) 
        [HttpGet("{id}/messages/{id2}")]
        public IActionResult getMessage(string id, int id2)
        {/*
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }*/
            if (_service.CheckContactByID(id, "david") && _service.CheckMessageByID(id, id2, "david"))
            {
                return Ok(_service.GetMessageById(id, id2, "david"));

            } else
            {
                return NotFound();
            }
        }
        // change a certain message(id2) from certain contact(id) 

        [HttpPut("{id}/messages/{id2}")]
        public IActionResult putMessage([Bind("content")] Message message,string id, int id2)
        {/*
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }*/
            if (_service.CheckContactByID(id, "david") && _service.CheckMessageByID(id, id2, "david"))
            {
                _service.GetMessageById(id, id2, "david").Content=message.Content;
                _service.GetMessageById(id, id2, "david").Created = DateTime.Now;
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        // delete a certain message(id2) from certain contact(id) 

        [HttpDelete("{id}/messages/{id2}")]
        public IActionResult deleteMessage(string id, int id2)
        {
            string user = HttpContext.Session.GetString("userName");
            /*if (user == null)
            {
                return BadRequest("User MUST Login!");
            }*/
            if (_service.CheckContactByID(id, "david") && _service.CheckMessageByID(id, id2, "david"))
            {
                _service.GetMessages(id, "david").Remove(_service.GetMessageById(id, id2, "david"));
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpPost("/api/invitations/")]
        public IActionResult Invite([Bind("from,to,server")] InvitationDetails details)
        {
            _service.Create(details.from, details.from, details.server, details.to);
            return Created(string.Format("/api/invitations/{0}", details.from), details.from);
        }
        [HttpPost("/api/transfer/")]
        public IActionResult Transfer([Bind("from,to,content")] TransferDetails t)
        {
            if (!_service.CheckContactByID(t.to, t.from))
            {
                return NotFound();
            }
            Message message = new Message();
            message.Id = _service.GetNextIdMessage(t.to, t.from);
            message.Created = DateTime.Now;
            message.Sent = false;
            message.Content = t.content;
            _service.GetMessages(t.to, t.from).Add(message);
            // change the info of the conatct
            _service.Get(t.to, t.from).Last = message.Content;
            _service.Get(t.to, t.from).LastDate = DateTime.Now;
            
            return Created(string.Format("/api/transfer/{0}", message.Id), message);

        }
    }

    }


