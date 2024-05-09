using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Service;
using System;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var person = await _personService.Get(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _personService.Add(person);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _personService.Update(id, person);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _personService.Delete(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string firstName = null, [FromQuery] string lastName = null, [FromQuery] string gender = null, [FromQuery] string birthPlace = null)
        {
            return Ok(await _personService.Filter(firstName, lastName, gender, birthPlace));
        }
    }
}
