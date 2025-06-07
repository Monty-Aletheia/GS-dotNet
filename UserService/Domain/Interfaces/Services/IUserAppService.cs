using UserService.App.Dtos.User;

namespace UserService.Domain.Interfaces.Services
{
	public interface IUserAppService
	{
		Task<UserDto> CreateUserAsync(CreateUserDto dto);
		Task<UserDto> CreateUserWithAddressAsync(CreateUserWithAddressDto dto);
		Task<UserDto?> GetUserByIdAsync(Guid id);
		Task<IEnumerable<UserDto>> GetAllUsersAsync();
		Task<UserDto> GetUserByFirebaseAsync(string email);
		Task<bool> UserExistsByEmailAsync(string email);
		Task UpdateUserAsync(Guid id, UpdateUserDto dto);
		Task DeleteUserAsync(Guid id);
	}
}