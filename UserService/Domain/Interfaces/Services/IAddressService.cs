using UserService.App.Dtos;
using UserService.App.Dtos.Address;

namespace UserService.Domain.Interfaces.Services
{
	public interface IAddressService
	{
		Task<AddressDto> CreateAddressAsync(CreateAddressDto input);
		Task<AddressDto?> GetAddressByIdAsync(Guid id);
		Task<IEnumerable<AddressDto>> GetAllAddressesAsync();
		Task UpdateAddressAsync(Guid id, UpdateAddressDto dto);
		Task DeleteAddressAsync(Guid id);
	}
}