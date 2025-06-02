using Shared.Interfaces;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces.Repositories
{
	public interface IDeviceRepository : IRepository<Device>
	{
		Task<bool> ExistsByExpoDeviceTokenAsync(string expoDeviceToken);
		Task<Device?> GetByExpoDeviceTokenAsync(string expoDeviceToken);
		Task<IEnumerable<Device>> GetAllByUserIdAsync(Guid userId);

		Task<IEnumerable<string>> GetTokensByCityAsync(string city);

		Task<bool> DeleteByExpoDeviceTokenAsync(string expoDeviceToken);

		Task<bool> DeleteByUserIdAsync(Guid userId);
	}
}