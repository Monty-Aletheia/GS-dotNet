using UserService.App.Dtos.Device;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces.Services
{
	public interface IDeviceService
	{
		Task<bool> ExistsByExpoDeviceTokenAsync(string expoDeviceToken);
		Task<Device?> GetByExpoDeviceTokenAsync(string expoDeviceToken);
		Task<IEnumerable<Device>> GetAllByUserIdAsync(Guid userId);
		Task<bool> DeleteByExpoDeviceTokenAsync(string expoDeviceToken);
		Task<bool> DeleteByUserIdAsync(Guid userId);
		Task<IEnumerable<string>> GetTokensByCityAsync(string city);

		Task<Device?> GetByIdAsync(Guid id);
		Task<IEnumerable<Device>> GetAllAsync();
		Task AddAsync(CreateDeviceDto entity);
		Task UpdateAsync(UpdateDeviceDto entity, Guid id);
		Task DeleteAsync(Guid id);
	}
}