using AutoMapper;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
        }
    }
}