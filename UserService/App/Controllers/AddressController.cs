using Microsoft.AspNetCore.Mvc;
using Shared.Errors;
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

		// Método auxiliar para gerar links HATEOAS
		private object GetAddressLinks(Guid id)
		{
			return new[]
			{
				new { rel = "self",    href = Url.Action(nameof(GetById),   new { id }) },
				new { rel = "update",  href = Url.Action(nameof(Update),    new { id }) },
				new { rel = "delete",  href = Url.Action(nameof(Delete),    new { id }) },
				new { rel = "all",     href = Url.Action(nameof(GetAll)) }
			};
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
				var response = new
				{
					address = created,
					links = GetAddressLinks(created.Id)
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


		// GET api/address/{id}
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var address = await _addressService.GetAddressByIdAsync(id);
			if (address == null)
				return NotFound();

			var response = new
			{
				address,
				links = GetAddressLinks(id)
			};

			return Ok(response);
		}

		// GET api/address
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var addresses = await _addressService.GetAllAddressesAsync();
			var response = addresses.Select(a => new
			{
				address = a,
				links = GetAddressLinks(a.Id)
			});
			return Ok(response);
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

		// DELETE api/address/{id}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _addressService.DeleteAddressAsync(id);
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