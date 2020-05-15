using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Entities;
using PostalCodeApi.Extensions;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    public class GeoNamesController : ControllerBase
    {
        private readonly IGeoNamesService _geoNamesService;

        public GeoNamesController(IGeoNamesService geoNamesService)
        {
            _geoNamesService = geoNamesService;
        }

        [Route("import")]
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Import([FromQuery] ImportGeoNamesResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            try
            {
                await _geoNamesService.ImportAsync(resource.CountryIso);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResource(StatusCodes.Status500InternalServerError, ex.Message));
            }

            return NoContent();
        }
    }
}