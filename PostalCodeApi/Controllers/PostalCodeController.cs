using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Extensions;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostalCodeController : Controller
    {
        private readonly IPostalCodeService _postalCodeService;
        private readonly IMapper _mapper;

        public PostalCodeController(IPostalCodeService postalCodeService, IMapper mapper)
        {
            _postalCodeService = postalCodeService;
            _mapper = mapper;
        }
        
        // @Todo Create error return 
        [HttpGet]
        public async Task<PagedAndSortedResponseResource<PostalCodeResource>> Search(
            [FromQuery] PagedAndSortedRequestResource pagedAndSortedRequest, string code, string countryIso)
        {
            var pagedAndSortedPostalCodes = await _postalCodeService.SearchAsync(pagedAndSortedRequest.PageNumber,
                pagedAndSortedRequest.PageSize, pagedAndSortedRequest.Sort, code, countryIso);
            var resources = _mapper.Map<PagedAndSortedList<PostalCode>, PagedAndSortedResponseResource<PostalCodeResource>>(pagedAndSortedPostalCodes);

            return resources;
        }
    }
}