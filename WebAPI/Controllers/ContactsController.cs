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
        public IEnumerable<Contact> Index()
        {

            return _service.GetAllContacts();
        }

        // GET: Articles/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            if (_service.CheckContactByID(id)){
                return Ok(_service.Get(id));
            } else
               return NotFound();
        }
        // GET: Articles/Details/5
        [HttpGet("{id}/messages")]
        public IActionResult DetailsMessages(string id)
        {
            if (_service.CheckContactByID(id))
            {
                return Ok(_service.Get(id).Messages);
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
            _service.Create(contact.Id, contact.Name, contact.Server);
            return Created(string.Format("/api/Contacts/{0}", contact.Id), contact);
        }

        [HttpPost("{id}/messages")]
        public IActionResult CreateMessage([Bind("Content")] Message message, string id)
        {
            message.Id = _service.GetNextIdMessage(id);
            _service.GetMessages(id).Add(message);
            return Created(string.Format("/api/Contacts/{0}/messages", message.Id), message);
        }

        [HttpPut("{id}")]

        public IActionResult Put([Bind("Name,Server")] Contact contact, string id)
        {
            if (_service.CheckContactByID(id))
            {
                _service.Get(id).Name = contact.Name;
                _service.Get(id).Server = contact.Server;
                return NoContent();
            }
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(string id)
        {
            if (_service.CheckContactByID(id))
            {
                _service.Delete(id);
                return NoContent();
            }
            else
                return NotFound();
        }
        [HttpGet("{id}/messages/{id2}")]
        public IActionResult getMessage(string id, int id2)
        {
            if (_service.CheckContactByID(id) && _service.CheckMessageByID(id, id2))
            {
                return Ok(_service.GetMessageById(id, id2));

            } else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}/messages/{id2}")]
        public IActionResult putMessage([Bind("content")] Message message,string id, int id2)
        {
            if (_service.CheckContactByID(id) && _service.CheckMessageByID(id, id2))
            {
                _service.GetMessageById(id, id2).Content=message.Content;
                _service.GetMessageById(id, id2).Created = DateTime.Now;
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
            if (_service.CheckContactByID(id) && _service.CheckMessageByID(id, id2))
            {
                _service.GetMessages(id).Remove(_service.GetMessageById(id, id2));
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }

    }


