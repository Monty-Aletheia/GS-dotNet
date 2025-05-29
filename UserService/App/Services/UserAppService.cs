using AutoMapper;
using UserService.App.Dtos;
using UserService.Domain.Models;
using UserService.Infra.Repositories;

namespace UserService.App.Services
{
	public class UserAppService : IUserAppService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UserAppService(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<UserDto> CreateUserAsync(CreateUserDto input)
		{
			if (await UserExistsByEmailAsync(input.Email))
				throw new Exception("Email already register.");

			var user = _mapper.Map<User>(input);

			await _userRepository.AddAsync(user);

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id)
		{
			var user = await _userRepository.GetByIdAsync(id);
			if (user == null) return null;

			return _mapper.Map<UserDto>(user);
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			var users = await _userRepository.GetAllAsync();

			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<bool> UserExistsByEmailAsync(string email)
		{
			return await _userRepository.ExistsByEmailAsync(email);
		}

		public async Task UpdateUserAsync(Guid id, UpdateUserDto dto)
		{
			var user = await _userRepository.GetByIdAsync(id);
			if (user == null)
				throw new Exception("User not found.");

			_mapper.Map(dto, user);

			await _userRepository.UpdateAsync(user);
		}


		public async Task DeleteUserAsync(Guid id)
		{
			var user = await _userRepository.GetByIdAsync(id);
			if (user == null)
				throw new Exception("User not found.");

			await _userRepository.DeleteAsync(user);
		}
	}
}