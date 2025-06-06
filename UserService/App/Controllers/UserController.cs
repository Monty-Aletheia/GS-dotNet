﻿using Microsoft.AspNetCore.Mvc;
using Shared.Errors;
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

		// links HATEOAS
		private object GetUserLinks(Guid id)
		{
			return new[]
			{
				new { rel = "self",    href = Url.Action(nameof(GetUserById),   new { id }) },
				new { rel = "update",  href = Url.Action(nameof(UpdateUser),    new { id }) },
				new { rel = "delete",  href = Url.Action(nameof(DeleteUser),    new { id }) },
				new { rel = "all",     href = Url.Action(nameof(GetAllUsers)) }
			};
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
				var response = new
				{
					user = createdUser,
					links = GetUserLinks(createdUser.Id)
				};
				return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, response);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error.", detail = ex.Message });
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
				var response = new
				{
					user = createdUser,
					links = GetUserLinks(createdUser.Id)
				};
				return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, response);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error.", detail = ex.Message });
			}
		}


		// GET api/user/{id}
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var user = await _userAppService.GetUserByIdAsync(id);
			if (user == null)
				return NotFound();

			var response = new
			{
				user,
				links = GetUserLinks(id)
			};

			return Ok(response);
		}


		// GET api/user
		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userAppService.GetAllUsersAsync();
			var response = users.Select(u => new
			{
				user = u,
				links = GetUserLinks(u.Id)
			});
			return Ok(response);
		}

		// GET api/user/byFirebaseId
		[HttpGet("/byFirebaseId/{firebaseId}")]
		public async Task<IActionResult> GetByFirebaseId(string firebaseId)
		{
			try
			{
				var user = await _userAppService.GetUserByFirebaseAsync(firebaseId);
				var response = new
				{
					user,
					links = GetUserLinks(user.Id)
				};
				return Ok(response);
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error.", detail = ex.Message });
			}
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
			catch (BadRequestException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error.", detail = ex.Message });
			}
		}


		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			try
			{
				await _userAppService.DeleteUserAsync(id);
				return NoContent();
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Internal server error.", detail = ex.Message });
			}
		}
	}
}