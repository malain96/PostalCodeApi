using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostalCodeController: Controller
    {
        private readonly IPostalCodeService _postalCodeService;
        private readonly IMapper _mapper;

        public PostalCodeController(IPostalCodeService postalCodeService, IMapper mapper)
        {
            _postalCodeService = postalCodeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PostalCodeResource>> Search()
        {
            var postalCodes = await _postalCodeService.SearchAsync();
            var resources = _mapper.Map<IEnumerable<PostalCode>, IEnumerable<PostalCodeResource>>(postalCodes);
            
            return resources;
        }
    }
}