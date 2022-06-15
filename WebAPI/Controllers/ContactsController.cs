using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services;
using System.Net.Http;
using WebAPI.Hubs;

namespace WebAPI.Controllers
{
    //the contoller is a service (api)
    [ApiController]
    [Route("/api/[controller]/")]

    public class ContactsController : Controller
    {
        private readonly IContactService _service;
        private readonly IHubContext<MyHub> _hub;
        public ContactsController(IContactService service, IHubContext<MyHub> hub)
        {
            _service = service;
            _hub = hub;
        }

        // Get all contacts of the current user

        [HttpGet]
        public IActionResult Index(string user)
        {

            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }

            return Ok(_service.GetAllContacts(user));
        }

        // Get a certain contact according to his id 

        [HttpGet("{id}")]
        public IActionResult Details(string id, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            if (_service.CheckContactByID(id, user))
            {
                return Ok(_service.Get(id, user));
            }
            else
                return NotFound();
        }

        // Get all messages from a certain contact (according to the id contact)
        [HttpGet("{id}/messages")]
        public IActionResult DetailsMessages(string id, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            if (_service.CheckContactByID(id, user))
            {
                return Ok(_service.Get(id, user).Messages);
            }
            else
                return NotFound();
        }
        // Get the last message from a certain contact (according to the id contact)

        [HttpGet("{id}/messages/lastMessage")]
        public IActionResult LastMessage(string id, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            if (_service.CheckContactByID(id, user))
            {
                var mess = _service.GetLastMessage(id, user);
                if (mess != null)
                {
                    return Ok(_service.GetLastMessage(id, user));
                }
                return NotFound("no messages yet!");
            }
            else
                return NotFound();
        }


        // add a new contact to the current user
        [HttpPost]

        public IActionResult CreateContact([Bind("Id,Name,Server")] Contact contact, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            _service.Create(contact.Id, contact.Name, contact.Server, user);
            return Created(string.Format("/api/Contacts/{0}", contact.Id), contact);
        }

        // add a new message to a certain contact
        [HttpPost("{id}/messages")]
        public IActionResult CreateMessage([Bind("Content")] Message message, string id, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            // check if the id contact exist
            if (!_service.CheckContactByID(id, user))
            {
                return NotFound();
            }

            message.Id = _service.GetNextIdMessage(id, user);
            message.Created = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
            message.Sent = true;
            _service.GetMessages(id, user).Add(message);
            // change the info of the conatct
            _service.Get(id, user).Last = message.Content;
            _service.Get(id, user).LastDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");

            return Created(string.Format("/api/Contacts/{0}/messages", message.Id), message);
        }

        [HttpPut("{id}")]
        //change a cerain contact info (only name and server)
        public IActionResult Put([Bind("Name,Server")] Contact contact, string id, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
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
        public IActionResult DeleteContact(string id, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
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
        public IActionResult getMessage(string id, int id2, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            if (_service.CheckContactByID(id, user) && _service.CheckMessageByID(id, id2, user))
            {
                return Ok(_service.GetMessageById(id, id2, user));

            }
            else
            {
                return NotFound();
            }
        }
        // change a certain message(id2) from certain contact(id) 

        [HttpPut("{id}/messages/{id2}")]
        public IActionResult putMessage([Bind("content")] Message message, string id, int id2, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            if (_service.CheckContactByID(id, user) && _service.CheckMessageByID(id, id2, user))
            {
                _service.GetMessageById(id, id2, user).Content = message.Content;
                _service.GetMessageById(id, id2, user).Created = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
                //update the contact info about the last message
                _service.Get(id, user).Last = _service.GetLastMessage(id, user).Content;
                _service.Get(id, user).LastDate = _service.GetLastMessage(id, user).Created;

                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        // delete a certain message(id2) from certain contact(id) 

        [HttpDelete("{id}/messages/{id2}")]
        public IActionResult deleteMessage(string id, int id2, string user)
        {
            if (!_service.CheckUserByID(user))
            {
                return BadRequest("This User is not Registered!");
            }
            if (_service.CheckContactByID(id, user) && _service.CheckMessageByID(id, id2, user))
            {
                _service.GetMessages(id, user).Remove(_service.GetMessageById(id, id2, user));
                //update the contact info about the last message
                _service.Get(id, user).Last = _service.GetLastMessage(id, user).Content;
                _service.Get(id, user).LastDate = _service.GetLastMessage(id, user).Created;

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
            if (!ContactService.contacts.ContainsKey(details.to) || details.from == details.to)
            {
                return NotFound();
            }
            if (_service.CheckContactByID(details.from, details.to))
            {
                return NotFound();
            }
            _service.Create(details.from, details.from, details.server, details.to);
            return Created(string.Format("/api/invitations/{0}", details.from), details);
        }
        [HttpPost("/api/transfer/")]
        public IActionResult Transfer([Bind("from,to,content")] TransferDetails t)
        {
            if (!_service.CheckContactByID(t.from, t.to))
            {
                return NotFound();
            }
            Message message = new Message();
            message.Id = _service.GetNextIdMessage(t.from, t.to);
            message.Created = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
            message.Sent = false;
            message.Content = t.content;
            _service.GetMessages(t.from, t.to).Add(message);
            // change the info of the conatct
            _service.Get(t.from, t.to).Last = message.Content;
            _service.Get(t.from, t.to).LastDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
            // _hub.Clients.All.SendAsync("ChangeRecevied", t.content, t.from, t.to);

            return Created(string.Format("/api/transfer/{0}", message.Id), message);

        }
    }

}


