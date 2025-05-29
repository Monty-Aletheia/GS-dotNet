using Shared.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infra.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<bool> ExistsByEmailAsync(string email);

	}
}