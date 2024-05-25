using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person> GetPerson(Guid id);
        Task PutPerson(Guid id, PersonDTO personDTO);
        Task PostPerson(PersonDTO personDTO);
        Task DeletePerson(Guid id);
        Task<IEnumerable<Person>> FilterPersons(string? name = null, string? gender = null, string? birthplace = null);
    }
}