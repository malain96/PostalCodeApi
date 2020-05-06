using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Extensions;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoNamesController: ControllerBase
    {
        private readonly IGeoNamesService _geoNamesService;

        public GeoNamesController(IGeoNamesService geoNamesService)
        {
            _geoNamesService = geoNamesService;
        }
        
        [Route("import")]
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [ProducesResponseType(typeof(ErrorResource), 500)]
        public async Task<IActionResult> Import([FromQuery] ImportGeoNamesResource resource)
        {
            if (!ModelState.IsValid)
                return Ok(new ErrorResource(400,ModelState.GetErrorMessages()));

            try
            {
                await _geoNamesService.ImportAsync(resource.CountryIso);
            }
            catch (Exception ex)
            {
                return Ok(new ErrorResource(500,ex.Message));
            }
            
            return Ok();
        }
    }
}