using System.Linq;
using AutoMapper;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Extensions;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<City, CityResource>();

            CreateMap<PostalCode, PostalCodeResource>().ForMember(
                dest => dest.Cities,
                opt => opt.MapFrom(src => src.PostalCodeCities.Select(pcc => pcc.City)));

            CreateMap<PagedAndSortedList<PostalCode>, PagedAndSortedResponseResource<PostalCodeResource>>();
            
            CreateMap<PagedAndSortedList<User>, PagedAndSortedResponseResource<UserResource>>();

            CreateMap<User, UserResource>();
        }
    }
}