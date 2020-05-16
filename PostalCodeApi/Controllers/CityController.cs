using System.Collections.Generic;
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
    public class CityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        private readonly IPostalCodeService _postalCodeService;

        public CityController(IMapper mapper, ICityService cityService, IPostalCodeService postalCodeService)
        {
            _mapper = mapper;
            _cityService = cityService;
            _postalCodeService = postalCodeService;
        }


        /// <summary>
        /// Get all cities for the given postal code
        /// </summary>
        /// <param name="resource">Search and country Iso</param>
        /// <returns>Response for the request</returns>
        [Route("PostalCode")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CityResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResource),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPostalCodeAsync([FromQuery] GetByPostalCodeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));
            
            var postalCode = _mapper.Map<GetByPostalCodeResource, PostalCode>(resource);
            var postalCodeResponse = await _postalCodeService.FindMatchOrFail(postalCode);

            if (!postalCodeResponse.Success)
                return StatusCode(postalCodeResponse.StatusCode,
                    new ErrorResource(postalCodeResponse.StatusCode, postalCodeResponse.Message));

            var cities = await _cityService.GetByPostalCodeAsync(postalCodeResponse.Resource.Id);
            var resources = _mapper.Map<List<City>, List<CityResource>>(cities);

            return Ok(resources);
        }
    }
}