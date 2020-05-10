using System.Threading.Tasks;
using AutoMapper;
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
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        ///  Get a user by id.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>Response for the request.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));
            
            var user = await _userService.GetByIdAsync(id);

            var userResource = _mapper.Map<User, UserResource>(user);
            return Ok(userResource);
        }

        /// <summary>
        ///  Get all users.
        /// </summary>
        /// <param name="pagedAndSortedRequest">Data to sort and page data.</param>
        /// <returns>Response for the request.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedAndSortedResponseResource<UserResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListAllAsync([FromQuery] PagedAndSortedRequestResource pagedAndSortedRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var pagedAndSortedUsers = await _userService.GetAllAsync(pagedAndSortedRequest.PageNumber,
                pagedAndSortedRequest.PageSize,
                pagedAndSortedRequest.Sort);

            var resources =
                _mapper.Map<PagedAndSortedList<User>, PagedAndSortedResponseResource<UserResource>>(pagedAndSortedUsers);

            return Ok(resources);
        }

        //@Todo update password
        //@Todo update isAdmin
        //@Todo delete

        /// <summary>
        ///  Saves a new user.
        /// </summary>
        /// <param name="resource">User data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveAsync(user, resource.Password);

            if (!result.Success)
            {
                if (result.InternalServerError)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ErrorResource(StatusCodes.Status500InternalServerError, result.Message));
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, result.Message));
            }

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return StatusCode(StatusCodes.Status201Created, userResource);
        }
    }
}