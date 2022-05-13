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

        [HttpGet]
        public IActionResult Index()
        {
            string user = HttpContext.Session.GetString("userName");

            if (HttpContext.Session.GetString("userName") == null)
            {
                return BadRequest();
            }
            return Ok(_service.GetAllContacts(user));
        }

        // GET: Articles/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            string user = HttpContext.Session.GetString("userName");

            if (_service.CheckContactByID(id, user)){
                return Ok(_service.Get(id, user));
            } else
               return NotFound();
        }
        // GET: Articles/Details/5
        [HttpGet("{id}/messages")]
        public IActionResult DetailsMessages(string id)
        {
            string user = HttpContext.Session.GetString("userName");
            if (_service.CheckContactByID(id, user))
            {
                return Ok(_service.Get(id, user).Messages);
            }
            else
                return NotFound();
        }


        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public IActionResult CreateContact([Bind("Id,Name,Server")] Contact contact)
        {   
            _service.Create(contact.Id, contact.Name, contact.Server, HttpContext.Session.GetString("userName"));
            return Created(string.Format("/api/Contacts/{0}", contact.Id), contact);
        }

        [HttpPost("{id}/messages")]
        public IActionResult CreateMessage([Bind("Content")] Message message, string id)
        {
            string user = HttpContext.Session.GetString("userName");

            message.Id = _service.GetNextIdMessage(id, user);
            _service.GetMessages(id, user).Add(message);
            return Created(string.Format("/api/Contacts/{0}/messages", message.Id), message);
        }

        [HttpPut("{id}")]

        public IActionResult Put([Bind("Name,Server")] Contact contact, string id)
        {
            string user = HttpContext.Session.GetString("userName");
            if (_service.CheckContactByID(id, user))
            {
                _service.Get(id, user).Name = contact.Name;
                _service.Get(id, user).Server = contact.Server;
                return NoContent();
            }
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            string user = HttpContext.Session.GetString("userName");

            if (_service.CheckContactByID(id, user))
            {
                _service.Delete(id, user);
                return NoContent();
            }
            else
                return NotFound();
        }
        [HttpGet("{id}/messages/{id2}")]
        public IActionResult getMessage(string id, int id2)
        {
            string user = HttpContext.Session.GetString("userName");

            if (_service.CheckContactByID(id, user) && _service.CheckMessageByID(id, id2, user))
            {
                return Ok(_service.GetMessageById(id, id2, user));

            } else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}/messages/{id2}")]
        public IActionResult putMessage([Bind("content")] Message message,string id, int id2)
        {
            string user = HttpContext.Session.GetString("userName");

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
        [HttpDelete("{id}/messages/{id2}")]
        public IActionResult deleteMessage(string id, int id2)
        {
            string user = HttpContext.Session.GetString("userName");

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
    }

    }


