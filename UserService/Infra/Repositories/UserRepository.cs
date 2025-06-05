using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Models;
using UserService.Infra.Data;

namespace UserService.Infra.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly OracleFiapContext _context;
		public UserRepository(OracleFiapContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> ExistsByEmailAsync(string email)
		{
			var normalizedEmail = email.ToLower();
			var count = await _context.Users
				.CountAsync(u => u.Email == normalizedEmail);
			return count > 0;
		}

		public async Task<bool> ExistsByIdAsync(Guid id)
		{
			var count = await _context.Users
				.CountAsync(u => u.Id == id);
			return count > 0;
		}


		public Task<User?> GetByFirebaseIdAsync(string id)
		{
			return _context.Users
				.FirstOrDefaultAsync(u => u.FirebaseId == id.ToString());
		}
	}
}