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
            if (_service.CheckByID(id)){
                return Ok(_service.Get(id));
            } else
               return NotFound();
        }
        // GET: Articles/Details/5
        [HttpGet("{id}/messages")]
        public IActionResult DetailsMessages(string id)
        {
            if (_service.CheckByID(id))
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
            message.Id = 150;
            _service.Get(id).Messages.Add(message);
            return Created(string.Format("/api/Contacts/{0}/messages", message.Id), message);
        }

        [HttpPut("{id}")]

        public IActionResult Put([Bind("Name,Server")] Contact contact, string id)
        {
            if (_service.CheckByID(id))
            {
                _service.Get(id).Name = contact.Name;
                _service.Get(id).Server = contact.Server;
                return NoContent();
            }
            else
                return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete2(string id)
        {
            if (_service.CheckByID(id))
            {
                _service.Delete(id);
                return NoContent();
            }
            else
                return NotFound();
        }
    }

    }


