using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces.Services;
using UserService.Domain.Models;
using UserService.Infra.Data;

namespace UserService.Infra.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly SqlServerContext _context;
		public UserRepository(SqlServerContext context) : base(context)
		{
			_context = context;
		}

		public Task<bool> ExistsByEmailAsync(string email)
		{
			var normalizedEmail = email.ToLower();
			return _context.Users
				.AnyAsync(u => u.Email.ToLower() == normalizedEmail);
		}

		public Task<bool> ExistsByIdAsync(Guid id)
		{
			return _context.Users
				.AnyAsync(u => u.Id == id);
		}
	}
}