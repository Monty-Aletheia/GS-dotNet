using Microsoft.AspNetCore.Mvc;
using UserService.App.Dtos;
using UserService.App.Dtos.Address;
using UserService.Domain.Interfaces.Services;

namespace UserService.App.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AddressController : ControllerBase
	{
		private readonly IAddressService _addressService;

		public AddressController(IAddressService addressService)
		{
			_addressService = addressService;
		}

		// POST api/address
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateAddressDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var created = await _addressService.CreateAddressAsync(dto);
				return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}

		}

		// GET api/address/{id}
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var address = await _addressService.GetAddressByIdAsync(id);
			if (address == null)
				return NotFound();

			return Ok(address);
		}

		// GET api/address
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var addresses = await _addressService.GetAllAddressesAsync();
			return Ok(addresses);
		}

		// PUT api/address/{id}
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAddressDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				await _addressService.UpdateAddressAsync(id, dto);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// DELETE api/address/{id}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _addressService.DeleteAddressAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}