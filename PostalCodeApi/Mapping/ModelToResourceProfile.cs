using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Extensions;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Mapping
{
    /// <summary>
    /// Mapper config from resource to model
    /// </summary>
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
            CreateMap<User, AuthResource>();
        }
    }
}