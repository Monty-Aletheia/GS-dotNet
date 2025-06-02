using AutoMapper;
using UserService.App.Dtos.Device;
using UserService.Domain.Models;

namespace UserService.App.Services.Mappers
{
	public class DeviceProfile : Profile
	{
		public DeviceProfile()
		{
			CreateMap<CreateDeviceDto, Device>();
			CreateMap<UpdateDeviceDto, Device>();
		}
	}
}