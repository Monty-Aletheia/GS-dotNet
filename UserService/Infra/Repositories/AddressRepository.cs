using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Models;
using UserService.Infra.Data;

namespace UserService.Infra.Repositories
{
	public class AddressRepository : Repository<Address>, IAddressRepository
	{
		private readonly OracleFiapContext _context;
		public AddressRepository(OracleFiapContext context) : base(context)
		{
			_context = context;
		}

	}
}