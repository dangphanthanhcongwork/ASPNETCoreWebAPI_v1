using WebApplication.Models;
using System.Threading.Tasks;

namespace WebApplication.Service
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> Get();
        Task<Person> Get(Guid id);
        Task Add(Person person);
        Task Update(Guid id, Person person);
        Task Delete(Guid id);
        Task<IEnumerable<Person>> Filter(string firstName, string lastName, string gender, string birthPlace);
    }
}
