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

        [HttpGet]
        [ProducesResponseType(typeof(PagedAndSortedResponseResource<PostalCodeResource>), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Search(
            [FromQuery] PagedAndSortedRequestResource pagedAndSortedRequest, [FromQuery] SearchPostalCodeResource inputResource)
        {
            if (!ModelState.IsValid)
                return Ok(new ErrorResource(400,ModelState.GetErrorMessages()));
            
            var pagedAndSortedPostalCodes = await _postalCodeService.SearchAsync(pagedAndSortedRequest.PageNumber,
                pagedAndSortedRequest.PageSize, pagedAndSortedRequest.Sort, inputResource.Code, inputResource.CountryIso);
            var resources = _mapper.Map<PagedAndSortedList<PostalCode>, PagedAndSortedResponseResource<PostalCodeResource>>(pagedAndSortedPostalCodes);

            return Ok(resources);
        }
    }
}