using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Models;
using UserService.Infra.Data;

namespace UserService.Infra.Repositories
{
	public class DeviceRepository : IDeviceRepository
	{
		private readonly SqlServerContext _context;

		public DeviceRepository(SqlServerContext context)
		{
			_context = context;
		}

		public async Task<bool> ExistsByExpoDeviceTokenAsync(string expoDeviceToken)
		{
			return await _context.Devices.AnyAsync(d => d.ExpoDeviceToken == expoDeviceToken);
		}

		public async Task<Device?> GetByExpoDeviceTokenAsync(string expoDeviceToken)
		{
			return await _context.Devices.FirstOrDefaultAsync(d => d.ExpoDeviceToken == expoDeviceToken);
		}

		public async Task<IEnumerable<Device>> GetAllByUserIdAsync(Guid userId)
		{
			return await _context.Devices.Where(d => d.userId == userId).ToListAsync();
		}

		public async Task<bool> DeleteByExpoDeviceTokenAsync(string expoDeviceToken)
		{
			var device = await _context.Devices.FirstOrDefaultAsync(d => d.ExpoDeviceToken == expoDeviceToken);
			if (device == null) return false;
			_context.Devices.Remove(device);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteByUserIdAsync(Guid userId)
		{
			var devices = await _context.Devices.Where(d => d.userId == userId).ToListAsync();
			if (!devices.Any()) return false;
			_context.Devices.RemoveRange(devices);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<string>> GetTokensByCityAsync(string city)
		{
			var tokens = await (from u in _context.Users
								join a in _context.Addresses on u.Id equals a.UserId
								join d in _context.Devices on u.Id equals d.userId
								where a.City == city
								select d.ExpoDeviceToken).ToListAsync();

			return tokens;
		}

		public async Task<Device?> GetByIdAsync(Guid id)
		{
			return await _context.Devices.FindAsync(id);
		}

		public async Task<IEnumerable<Device>> GetAllAsync()
		{
			return await _context.Devices.ToListAsync();
		}

		public async Task AddAsync(Device entity)
		{
			await _context.Devices.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Device entity)
		{
			_context.Devices.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Device entity)
		{
			_context.Devices.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}