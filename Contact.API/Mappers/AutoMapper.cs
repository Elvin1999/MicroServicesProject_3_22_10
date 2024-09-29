using AutoMapper;
using Contact.API.Dtos;
using Contact.API.Models;

namespace Contact.API.Mappers
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<ContactModel,ContactDto>().ReverseMap();
        }
    }
}
