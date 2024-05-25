using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonsController(IPersonService service)
        {
            _service = service;
        }

        // GET: api/persons
        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            var persons = await _service.GetPersons();
            return Ok(persons);
        }

        // GET: api/persons/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            try
            {
                var person = await _service.GetPerson(id);
                return Ok(person);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/persons/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonDTO personDTO)
        {
            try
            {
                await _service.PutPerson(id, personDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPerson(PersonDTO personDTO)
        {
            await _service.PostPerson(personDTO);
            return CreatedAtAction(nameof(GetPerson), new { id = Guid.NewGuid() }, personDTO);
        }

        // DELETE: api/persons/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            try
            {
                await _service.DeletePerson(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Person>>> FilterPersons([FromQuery] string? name, [FromQuery] string? gender, [FromQuery] string? birthplace)
        {
            var filteredPersons = await _service.FilterPersons(name, gender, birthplace);
            return Ok(filteredPersons);
        }
    }
}