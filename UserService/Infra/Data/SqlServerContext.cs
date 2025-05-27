using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.Infra.Data
{
	public class SqlServerContext : DbContext
	{
		public SqlServerContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
	}
}