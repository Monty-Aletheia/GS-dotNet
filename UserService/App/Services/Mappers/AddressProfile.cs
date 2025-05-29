using AutoMapper;
using UserService.App.Dtos;
using UserService.App.Dtos.Address;
using UserService.Domain.Models;

namespace UserService.App.Services.Mappers
{
	public class AddressProfile : Profile
	{

		public AddressProfile()
		{

			CreateMap<CreateAddressDto, Address>().ReverseMap();
			CreateMap<Address, AddressDto>().ReverseMap();
			CreateMap<UpdateAddressDto, AddressDto>().ReverseMap();
			CreateMap<CreateAddressWithUserDto, Address>();
		}
	}
}