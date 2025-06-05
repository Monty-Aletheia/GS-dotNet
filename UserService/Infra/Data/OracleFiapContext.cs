using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.Infra.Data
{
	public class OracleFiapContext : DbContext
	{
		public OracleFiapContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Device> Devices { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Address - User (1:1)
			modelBuilder.Entity<Address>()
				.HasOne(a => a.User)
				.WithOne(u => u.Address)
				.HasForeignKey<Address>(a => a.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			// Device - User (N:1)
			modelBuilder.Entity<Device>()
				.HasOne(d => d.User)
				.WithMany(u => u.Devices)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
		}
	}
}