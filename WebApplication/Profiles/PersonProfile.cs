using AutoMapper;
using WebApplication.Models;
using WebApplication.DTOs;

namespace WebApplication.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();
        }
    }
}