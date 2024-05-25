using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _repository.GetPersons();
        }

        public async Task<Person> GetPerson(Guid id)
        {
            try
            {
                return await _repository.GetPerson(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutPerson(Guid id, PersonDTO personDTO)
        {
            try
            {
                var person = _mapper.Map<Person>(personDTO);
                await _repository.PutPerson(id, person);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostPerson(PersonDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);
            await _repository.PostPerson(person);
        }

        public async Task DeletePerson(Guid id)
        {
            try
            {
                await _repository.DeletePerson(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Person>> FilterPersons(string? name = null, string? gender = null, string? birthplace = null)
        {
            return await _repository.FilterPersons(name, gender, birthplace);
        }
    }
}