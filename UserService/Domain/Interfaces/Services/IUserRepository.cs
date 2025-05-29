using Shared.Interfaces;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces.Services
{
	public interface IUserRepository : IRepository<User>
	{
		Task<bool> ExistsByEmailAsync(string email);

		Task<bool> ExistsByIdAsync(Guid id);
	}
}