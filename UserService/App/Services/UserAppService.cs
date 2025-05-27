using UserService.App.Dtos;
using UserService.Domain.Models;
using UserService.Infra.Repositories;

namespace UserService.App.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto input)
        {
            if (await _userRepository.ExistsByEmailAsync(input.Email))
                throw new Exception("Email already register.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Email = input.Email,
                Password = input.Password 
            };

            await _userRepository.AddAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var result = new List<UserDto>();

            foreach (var user in users)
            {
                result.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                });
            }

            return result;
        }

		public Task<bool> UserExistsByEmailAsync(string email)
		{
			throw new NotImplementedException();
		}
	}
}
