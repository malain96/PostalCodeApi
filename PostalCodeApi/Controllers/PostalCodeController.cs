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
    public class PostalCodeController : Controller
    {
        private readonly IPostalCodeService _postalCodeService;
        private readonly IMapper _mapper;

        public PostalCodeController(IPostalCodeService postalCodeService, IMapper mapper)
        {
            _postalCodeService = postalCodeService;
            _mapper = mapper;
        }

        
        // @Todo Put searching paging and sorting in a resource
        // @Todo Create a pagedResource response (+sorting) (page num, count, size, has next, has previous)
        [HttpGet]
        public async Task<IEnumerable<PostalCodeResource>> Search(int? pageNumber, int? pageSize, string sort, string code, string countryIso)
        {
            var currentSort = sort ?? "asc";
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            var postalCodes = await _postalCodeService.SearchAsync(currentPageNumber, currentPageSize, currentSort, code, countryIso);
            var resources = _mapper.Map<IEnumerable<PostalCode>, IEnumerable<PostalCodeResource>>(postalCodes);

            return resources;
        }
    }
}