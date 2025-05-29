using UserService.App.Dtos;

namespace UserService.App.Services
{
	public interface IUserAppService
	{
		Task<UserDto> CreateUserAsync(CreateUserDto dto);
		Task<UserDto?> GetUserByIdAsync(Guid id);
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<bool> UserExistsByEmailAsync(string email);
		Task UpdateUserAsync(Guid id, UpdateUserDto dto);
		Task DeleteUserAsync(Guid id);
	}
}