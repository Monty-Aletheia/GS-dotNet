using Microsoft.AspNetCore.Mvc;
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

		// POST api/device
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateDeviceDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _deviceService.AddAsync(dto);
			return Created("", null);
		}

		// GET api/device/{id}
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var device = await _deviceService.GetByIdAsync(id);
			if (device == null)
				return NotFound();

			return Ok(device);
		}

		// GET api/device
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var devices = await _deviceService.GetAllAsync();
			return Ok(devices);
		}

		// GET api/device/byUser/{userId}
		[HttpGet("byUser/{userId:guid}")]
		public async Task<IActionResult> GetByUserId(Guid userId)
		{
			var devices = await _deviceService.GetAllByUserIdAsync(userId);
			return Ok(devices);
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

			await _deviceService.UpdateAsync(dto, id);
			return NoContent();
		}

		// DELETE api/device/{id}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _deviceService.DeleteAsync(id);
			return NoContent();
		}
	}
}