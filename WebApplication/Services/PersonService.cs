using WebApplication.Models;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class PersonService
    {
        private static readonly List<Person> persons = new List<Person>
        {
            // add dummy data
            new Person { Id = Guid.NewGuid(), FirstName = "Công", LastName = "Đặng Phan Thành", Gender = Gender.Male, DateOfBirth = new DateTime(2000, 6, 15), PhoneNumber = "0375284637", BirthPlace = "Lâm Đồng", IsGraduated = true },
            new Person { Id = Guid.NewGuid(), FirstName = "Linh", LastName = "Nguyễn Mỹ", Gender = Gender.Female, DateOfBirth = new DateTime(1995, 7, 4), PhoneNumber = "0375284636", BirthPlace = "Hà Nội", IsGraduated = true },
            new Person { Id = Guid.NewGuid(), FirstName = "Phương", LastName = "Nguyễn Thị Mai", Gender = Gender.Female, DateOfBirth = new DateTime(2002, 4, 7), PhoneNumber = "0375284635", BirthPlace = "Hải Phòng", IsGraduated = false },
            new Person { Id = Guid.NewGuid(), FirstName = "Thu", LastName = "Phan Thị Hà", Gender = Gender.Female, DateOfBirth = new DateTime(2003, 2, 27), PhoneNumber = "0375284634", BirthPlace = "Huế", IsGraduated = false },
            new Person { Id = Guid.NewGuid(), FirstName = "Quang", LastName = "Trần Huy", Gender = Gender.Male, DateOfBirth = new DateTime(1994, 4, 20), PhoneNumber = "0375284633", BirthPlace = "Hà Nội", IsGraduated = false },
        };

        public async Task<IEnumerable<Person>> Get()
        {
            return await Task.FromResult(persons.AsEnumerable());
        }

        public async Task<Person> Get(Guid id)
        {
            return await Task.FromResult(persons.FirstOrDefault(p => p.Id == id));
        }

        public async Task Add(Person person)
        {
            Guid newId = Guid.NewGuid();
            while (persons.Any(p => p.Id == newId))
            {
                newId = Guid.NewGuid();
            }

            person.Id = newId;
            persons.Add(person);
            await Task.CompletedTask;
        }

        public async Task Update(Guid id, Person person)
        {
            var existingPerson = persons.FirstOrDefault(p => p.Id == id);
            if (existingPerson != null)
            {
                existingPerson = person;
            }
            await Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                persons.Remove(person);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Person>> Filter(string? firstName = null, string? lastName = null, string? gender = null, string? birthPlace = null)
        {
            var filteredPersons = persons.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                filteredPersons = filteredPersons.Where(p => p.FirstName.Contains(firstName, StringComparison.Ordinal));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                filteredPersons = filteredPersons.Where(p => p.FirstName.Contains(lastName, StringComparison.Ordinal));
            }

            if (!string.IsNullOrEmpty(gender))
            {
                filteredPersons = filteredPersons.Where(p => p.Gender.ToString().Equals(gender, StringComparison.Ordinal));
            }

            if (!string.IsNullOrEmpty(birthPlace))
            {
                filteredPersons = filteredPersons.Where(p => p.BirthPlace.Contains(birthPlace, StringComparison.Ordinal));
            }

            return await Task.FromResult(filteredPersons.ToList());
        }
    }
}
