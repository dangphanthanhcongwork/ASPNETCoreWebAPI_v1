using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetPerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id) ?? throw new Exception("Not found!!!");
            return person;
        }

        public async Task PutPerson(Guid id, Person person)
        {
            _context.Persons.Entry(person).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PersonExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PersonExists(Guid id)
        {
            return await _context.Persons.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Person>> FilterPersons(string? name = null, string? gender = null, string? birthplace = null)
        {
            var filteredPersons = _context.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                filteredPersons = filteredPersons.Where(p => (p.FirstName + " " + p.LastName).Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(gender))
            {
                filteredPersons = filteredPersons.Where(p => p.Gender.ToString().Equals(gender, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(birthplace))
            {
                filteredPersons = filteredPersons.Where(p => p.Birthplace.Contains(birthplace, StringComparison.OrdinalIgnoreCase));
            }

            return await filteredPersons.ToListAsync();
        }
    }
}