using AutoMapper;
using Shared.Errors;
using UserService.App.Dtos.Device;
using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Interfaces.Services;
using UserService.Domain.Models;

namespace UserService.App.Services
{
	public class DeviceService : IDeviceService
	{
		private readonly IDeviceRepository _deviceRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public DeviceService(IDeviceRepository deviceRepository, IMapper mapper, IUserRepository userRepository)
		{
			_deviceRepository = deviceRepository;
			_mapper = mapper;
			_userRepository = userRepository;
		}

		public async Task<bool> ExistsByExpoDeviceTokenAsync(string expoDeviceToken)
			=> await _deviceRepository.ExistsByExpoDeviceTokenAsync(expoDeviceToken);

		public async Task<Device?> GetByExpoDeviceTokenAsync(string expoDeviceToken)
			=> await _deviceRepository.GetByExpoDeviceTokenAsync(expoDeviceToken);

		public async Task<IEnumerable<Device>> GetAllByUserIdAsync(Guid userId)
			=> await _deviceRepository.GetAllByUserIdAsync(userId);

		public async Task<bool> DeleteByExpoDeviceTokenAsync(string expoDeviceToken)
			=> await _deviceRepository.DeleteByExpoDeviceTokenAsync(expoDeviceToken);

		public async Task<bool> DeleteByUserIdAsync(Guid userId)
			=> await _deviceRepository.DeleteByUserIdAsync(userId);

		public async Task<IEnumerable<string>> GetTokensByCityAsync(string city)
			=> await _deviceRepository.GetTokensByCityAsync(city);

		public async Task<Device?> GetByIdAsync(Guid id)
			=> await _deviceRepository.GetByIdAsync(id);

		public async Task<IEnumerable<Device>> GetAllAsync()
			=> await _deviceRepository.GetAllAsync();

		public async Task<Device> CreateAsync(CreateDeviceDto dto)
		{
			var userExists = await _userRepository.ExistsByIdAsync(dto.UserId);
			if (!userExists)
				throw new BadRequestException("User does not exist.");

			var entity = _mapper.Map<Device>(dto);
			await _deviceRepository.AddAsync(entity);
			return entity;
		}

		public async Task UpdateAsync(UpdateDeviceDto dto, Guid id)
		{
			var userExists = await _userRepository.ExistsByIdAsync(dto.UserId);
			if (!userExists)
				throw new BadRequestException("User does not exist.");

			var device = await _deviceRepository.GetByIdAsync(id);
			if (device == null)
				throw new NotFoundException("Device not found.");


			var entityToUpdate = _mapper.Map(dto, device);
			await _deviceRepository.UpdateAsync(entityToUpdate);
		}

		public async Task DeleteAsync(Guid id)
		{
			var device = await _deviceRepository.GetByIdAsync(id);
			if (device == null)
				throw new NotFoundException("Device not found.");
			await _deviceRepository.DeleteAsync(device);
		}
	}
}