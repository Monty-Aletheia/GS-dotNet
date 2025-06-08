using Microsoft.AspNetCore.Mvc;
using Shared.Errors;
using UserService.App.Dtos.Device;
using UserService.Domain.Interfaces.Services;

namespace UserService.App.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DeviceController : ControllerBase
	{
		private readonly IDeviceService _deviceService;

		public DeviceController(IDeviceService deviceService)
		{
			_deviceService = deviceService;
		}

		// Método auxiliar para gerar links HATEOAS
		private object GetDeviceLinks(Guid id)
		{
			return new[]
			{
				new { rel = "self",    href = Url.Action(nameof(GetById),   new { id }) },
				new { rel = "update",  href = Url.Action(nameof(Update),    new { id }) },
				new { rel = "delete",  href = Url.Action(nameof(Delete),    new { id }) },
				new { rel = "all",     href = Url.Action(nameof(GetAll)) }
			};
		}

		// POST api/device
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateDeviceDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var created = await _deviceService.CreateAsync(dto);
				var response = new
				{
					device = created,
					links = GetDeviceLinks(created.Id)
				};
				return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
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

		// GET api/device/{id}
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var device = await _deviceService.GetByIdAsync(id);
			if (device == null)
				return NotFound();

			var response = new
			{
				device,
				links = GetDeviceLinks(id)
			};

			return Ok(response);
		}

		// GET api/device
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var devices = await _deviceService.GetAllAsync();
			var response = devices.Select(d => new
			{
				device = d,
				links = GetDeviceLinks(d.Id)
			});
			return Ok(response);
		}

		// GET api/device/byUser/{userId}
		[HttpGet("byUser/{userId:guid}")]
		public async Task<IActionResult> GetByUserId(Guid userId)
		{
			var devices = await _deviceService.GetAllByUserIdAsync(userId);
			var response = devices.Select(d => new
			{
				device = d,
				links = GetDeviceLinks(d.Id)
			});
			return Ok(response);
		}

		// GET api/device/tokensByCity/{city}
		[HttpGet("tokensByCity/{city}")]
		public async Task<IActionResult> GetTokensByCity(string city)
		{
			var tokens = await _deviceService.GetTokensByCityAsync(city);
			return Ok(tokens);
		}

		// PUT api/device/{id}
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDeviceDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				await _deviceService.UpdateAsync(dto, id);
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

		// DELETE api/device/{id}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _deviceService.DeleteAsync(id);
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