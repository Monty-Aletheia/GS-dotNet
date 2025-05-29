using AutoMapper;
using Shared.Errors;
using UserService.App.Dtos;
using UserService.App.Dtos.Address;
using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Interfaces.Services;
using UserService.Domain.Models;

namespace UserService.App.Services
{
	public class AddressService : IAddressService
	{
		private readonly IAddressRepository _repository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public AddressService(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper)
		{
			_repository = addressRepository;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<AddressDto> CreateAddressAsync(CreateAddressDto input)
		{
			var userExists = await _userRepository.ExistsByIdAsync(input.UserId);
			if (!userExists)
				throw new BadRequestException("User does not exist.");

			var address = _mapper.Map<Address>(input);
			await _repository.AddAsync(address);
			return _mapper.Map<AddressDto>(address);
		}

		public async Task<AddressDto> GetAddressByIdAsync(Guid id)
		{
			var address = await _repository.GetByIdAsync(id);
			if (address == null) return null;
			return _mapper.Map<AddressDto>(address);
		}

		public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
		{
			var addresses = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<AddressDto>>(addresses);
		}

		public async Task UpdateAddressAsync(Guid id, UpdateAddressDto dto)
		{
			var address = await _repository.GetByIdAsync(id);
			if (address == null)
				throw new NotFoundException("Address not found.");
			_mapper.Map(dto, address);
			await _repository.UpdateAsync(address);
		}

		public async Task DeleteAddressAsync(Guid id)
		{
			var address = await _repository.GetByIdAsync(id);
			if (address == null)
				throw new NotFoundException("Address not found.");
			await _repository.DeleteAsync(address);
		}
	}
}