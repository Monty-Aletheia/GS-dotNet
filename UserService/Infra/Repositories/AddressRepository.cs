using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Models;
using UserService.Infra.Data;

namespace UserService.Infra.Repositories
{
	public class AddressRepository : Repository<Address>, IAddressRepository
	{
		private readonly SqlServerContext _context;
		public AddressRepository(SqlServerContext context) : base(context)
		{
			_context = context;
		}

	}
}