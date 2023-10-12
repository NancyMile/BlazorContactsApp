using BlazorApp.Data;
using BlazorApp2.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // GET: api/Contacts
        [HttpGet]
        public  async Task<IEnumerable<Contact>> Get()
        {
            return await _contactRepository.GetAll();
        }

        // GET api/Contacts/5
        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            return await _contactRepository.GetDetails(id);
        }

        // POST api/Contacts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            //validations
            if (contact == null)
            {
                return BadRequest();
            }
            // custom validation
            if (string.IsNullOrEmpty(contact.LastName))
                ModelState.AddModelError("Contact", "Last name can't be empty");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRepository.Insert(contact);

            return NoContent();
        }

        // PUT api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Contact contact)
        {
            //validations
            if (contact == null)
            {
                return BadRequest();
            }
            // custom validation
            if (string.IsNullOrEmpty(contact.LastName))
                ModelState.AddModelError("Contact", "Last name can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (contact.Id == 0)
                contact.Id = id;

            await _contactRepository.Update(contact);

            return NoContent();
        }

        // DELETE api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.Delete(id);
            return NoContent();
        }
    }
}
