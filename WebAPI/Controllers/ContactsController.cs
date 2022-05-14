using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

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

            if (HttpContext.Session.GetString("userName") == null)
            {
                return BadRequest("User MUST Login!");
            }
            return Ok(_service.GetAllContacts(user));
        }

        // Get a certain contact according to his id 

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (_service.CheckContactByID(id, user)){
                return Ok(_service.Get(id, user));
            } else
               return NotFound();
        }

        // Get all messages from a certain contact (according to the id contact)
        [HttpGet("{id}/messages")]
        public IActionResult DetailsMessages(string id)
        {
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (_service.CheckContactByID(id, user))
            {
                return Ok(_service.Get(id, user).Messages);
            }
            else
                return NotFound();
        }


        // add a new contact to the current user
        [HttpPost]

        public IActionResult CreateContact([Bind("Id,Name,Server")] Contact contact)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return BadRequest("User MUST Login!");
            }
            _service.Create(contact.Id, contact.Name, contact.Server, HttpContext.Session.GetString("userName"));
            return Created(string.Format("/api/Contacts/{0}", contact.Id), contact);
        }

        // add a new message to a certain contact
        [HttpPost("{id}/messages")]
        public IActionResult CreateMessage([Bind("Content")] Message message, string id)
        {
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (!_service.CheckContactByID(id, user))
            {
                return NotFound();
            }
            
            message.Id = _service.GetNextIdMessage(id, user);
            message.Created = DateTime.Now;
            _service.GetMessages(id, user).Add(message);
            _service.Get(id, user).Last = message.Content;
            _service.Get(id, user).LastDate = DateTime.Now;

            return Created(string.Format("/api/Contacts/{0}/messages", message.Id), message);
        }

        [HttpPut("{id}")]
        //change a cerain contact info (only name and server)
        public IActionResult Put([Bind("Name,Server")] Contact contact, string id)
        {
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (_service.CheckContactByID(id, user))
            {
                _service.Get(id, user).Name = contact.Name;
                _service.Get(id, user).Server = contact.Server;
                return NoContent();
            }
            else
                return NotFound();
        }
        // delete a certain contact 
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (_service.CheckContactByID(id, user))
            {
                _service.Delete(id, user);
                return NoContent();
            }
            else
                return NotFound();
        }
        // get a certain message(id2) from certain contact(id) 
        [HttpGet("{id}/messages/{id2}")]
        public IActionResult getMessage(string id, int id2)
        {
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (_service.CheckContactByID(id, user) && _service.CheckMessageByID(id, id2, user))
            {
                return Ok(_service.GetMessageById(id, id2, user));

            } else
            {
                return NotFound();
            }
        }
        // change a certain message(id2) from certain contact(id) 

        [HttpPut("{id}/messages/{id2}")]
        public IActionResult putMessage([Bind("content")] Message message,string id, int id2)
        {
            string user = HttpContext.Session.GetString("userName");
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (_service.CheckContactByID(id, user) && _service.CheckMessageByID(id, id2, user))
            {
                _service.GetMessageById(id, id2, user).Content=message.Content;
                _service.GetMessageById(id, id2, user).Created = DateTime.Now;
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
            if (user == null)
            {
                return BadRequest("User MUST Login!");
            }
            if (_service.CheckContactByID(id, user) && _service.CheckMessageByID(id, id2, user))
            {
                _service.GetMessages(id, user).Remove(_service.GetMessageById(id, id2, user));
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        /*
        [HttpPost("/transfer/")]
        public IActionResult Invite( string from, string to, string server)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(server);
            http.PostAsJsonAsync(from, to).Wait();
        }
        */
    }

    }


