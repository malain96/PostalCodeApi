using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostalCodeApi.Domain.Models;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Entities;
using PostalCodeApi.Extensions;
using PostalCodeApi.Resources;

namespace PostalCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        /// Get a user by id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>Response for the request</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Get all users
        /// </summary>
        /// <param name="pagedAndSortedRequest">Data to sort and page data</param>
        /// <returns>Response for the request</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [ProducesResponseType(typeof(PagedAndSortedResponseResource<UserResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListAllAsync([FromQuery] PagedAndSortedRequestResource pagedAndSortedRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var pagedAndSortedUsers = await _userService.GetAllAsync(pagedAndSortedRequest.PageNumber,
                pagedAndSortedRequest.PageSize,
                pagedAndSortedRequest.Sort);

            var resources =
                _mapper.Map<PagedAndSortedList<User>, PagedAndSortedResponseResource<UserResource>>(
                    pagedAndSortedUsers);

            return Ok(resources);
        }

        /// <summary>
        /// Update the password of a user
        /// </summary>
        /// <param name="resource">Old and new password</param>
        /// <returns>Response for the request</returns>
        [Route("Password")]
        [HttpPatch]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var id = 0;
            if (HttpContext.User.Identity is ClaimsIdentity identity)
                id = int.Parse(identity.FindFirst("UserId").Value);

            var response = await _userService.UpdatePasswordAsync(id, resource.OldPassword, resource.NewPassword);

            if (response.Success == false)
                return StatusCode(response.StatusCode,
                    new ErrorResource(response.StatusCode, response.Message));

            var userResource = _mapper.Map<User, UserResource>(response.Resource);
            return Ok(userResource);
        }

        /// <summary>
        /// Update the role of a user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="resource">The new role</param>
        /// <returns>Response for the request.</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute] int id, [FromBody] UpdateRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var response = await _userService.UpdateRoleAsync(id, resource.Role);

            if (response.Success == false)
                return StatusCode(response.StatusCode,
                    new ErrorResource(response.StatusCode, response.Message));

            var userResource = _mapper.Map<User, UserResource>(response.Resource);
            return Ok(userResource);
        }

        /// <summary>
        /// Delete a user by id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>Response for the request</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var response = await _userService.DeleteAsync(id);

            if (response.Success == false)
                return StatusCode(response.StatusCode,
                    new ErrorResource(response.StatusCode, response.Message));

            var userResource = _mapper.Map<User, UserResource>(response.Resource);
            return Ok(userResource);
        }

        /// <summary>
        /// Saves a new user
        /// </summary>
        /// <param name="resource">User data</param>
        /// <returns>Response for the request</returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [ProducesResponseType(typeof(UserResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResource), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResource(StatusCodes.Status400BadRequest, ModelState.GetErrorMessages()));

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var response = await _userService.SaveAsync(user, resource.Password);

            if (response.Success == false)
                return StatusCode(response.StatusCode,
                    new ErrorResource(response.StatusCode, response.Message));

            var userResource = _mapper.Map<User, UserResource>(response.Resource);
            return StatusCode(response.StatusCode, userResource);
        }
    }
}