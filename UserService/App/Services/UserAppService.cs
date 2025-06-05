using AutoMapper;
using Shared.Errors;
using UserService.App.Dtos.User;
using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Interfaces.Services;
using UserService.Domain.Models;

namespace UserService.App.Services
{
	public class UserAppService : IUserAppService
	{
		private readonly IUserRepository _repository;
		private readonly IMapper _mapper;

		public UserAppService(IUserRepository userRepository, IMapper mapper)
		{
			_repository = userRepository;
			_mapper = mapper;
		}

		public async Task<UserDto> CreateUserAsync(CreateUserDto input)
		{
			if (await UserExistsByEmailAsync(input.Email))
				throw new BadRequestException("Email already register.");

			var user = _mapper.Map<User>(input);

			user.Email = user.Email.ToLower();

			await _repository.AddAsync(user);

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> CreateUserWithAddressAsync(CreateUserWithAddressDto dto)
		{
			if (await UserExistsByEmailAsync(dto.Email))
				throw new BadRequestException("Email already register.");

			var user = _mapper.Map<User>(dto);

			var address = _mapper.Map<Address>(dto.Address);

			user.Address = address;

			await _repository.AddAsync(user);

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id)
		{
			var user = await _repository.GetByIdAsync(id);
			if (user == null) return null;

			return _mapper.Map<UserDto>(user);
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			var users = await _repository.GetAllAsync();

			return _mapper.Map<IEnumerable<UserDto>>(users);
		}
		public async Task<UserDto> GetUserByFirebaseAsync(string firebaseId)
		{
			var user = await _repository.GetByFirebaseIdAsync(firebaseId);
			if(user == null) throw new NotFoundException("User not found.");

			return _mapper.Map<UserDto>(user);
		}

		public async Task<bool> UserExistsByEmailAsync(string email)
		{
			return await _repository.ExistsByEmailAsync(email);
		}

		public async Task UpdateUserAsync(Guid id, UpdateUserDto dto)
		{
			var user = await _repository.GetByIdAsync(id);
			if (user == null)
				throw new NotFoundException("User not found.");

			_mapper.Map(dto, user);

			await _repository.UpdateAsync(user);
		}


		public async Task DeleteUserAsync(Guid id)
		{
			var user = await _repository.GetByIdAsync(id);
			if (user == null)
				throw new NotFoundException("User not found.");

			await _repository.DeleteAsync(user);
		}
	}
}