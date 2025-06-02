using Microsoft.AspNetCore.Mvc;
using UserService.App.Dtos.User;
using UserService.Domain.Interfaces.Services;

namespace UserService.App.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserAppService _userAppService;

		public UserController(IUserAppService userAppService)
		{
			_userAppService = userAppService;
		}

		// POST api/user
		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var createdUser = await _userAppService.CreateUserAsync(dto);
				return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// POST api/user/withAddress
		[HttpPost("withAddress")]
		public async Task<IActionResult> CreateUserWithAddress([FromBody] CreateUserWithAddressDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var createdUser = await _userAppService.CreateUserWithAddressAsync(dto);
				return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// GET api/user/{id}
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var user = await _userAppService.GetUserByIdAsync(id);
			if (user == null)
				return NotFound();

			return Ok(user);
		}

		// GET api/user
		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userAppService.GetAllUsersAsync();
			return Ok(users);
		}

		// PUT api/user/{id}
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				await _userAppService.UpdateUserAsync(id, dto);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// DELETE api/user/{id}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			try
			{
				await _userAppService.DeleteUserAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}