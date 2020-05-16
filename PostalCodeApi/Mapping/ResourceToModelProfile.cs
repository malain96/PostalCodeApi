using AutoMapper;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Mapping
{
    /// <summary>
    /// Mapper config from resource to model
    /// </summary>
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<GetByPostalCodeResource, PostalCode>();
        }
    }
}