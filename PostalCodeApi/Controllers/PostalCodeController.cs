using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Extensions;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostalCodeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostalCodeService _postalCodeService;

        public PostalCodeController(IPostalCodeService postalCodeService, IMapper mapper)
        {
            _postalCodeService = postalCodeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedAndSortedResponseResource<PostalCodeResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search(
            [FromQuery] PagedAndSortedRequestResource pagedAndSortedRequest,
            [FromQuery] SearchPostalCodeResource inputResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var pagedAndSortedPostalCodes = await _postalCodeService.SearchAsync(pagedAndSortedRequest.PageNumber,
                pagedAndSortedRequest.PageSize, pagedAndSortedRequest.Sort, inputResource.Code,
                inputResource.CountryIso);
            var resources =
                _mapper.Map<PagedAndSortedList<PostalCode>, PagedAndSortedResponseResource<PostalCodeResource>>(
                    pagedAndSortedPostalCodes);

            return Ok(resources);
        }
    }
}