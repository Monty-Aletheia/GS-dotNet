using AutoMapper;
using UserService.App.Dtos;
using UserService.Domain.Models;

namespace UserService.App.Services.Mappers
{
	public class UserProfile : Profile
	{
		public UserProfile() {
			CreateMap<CreateUserDto, User>().ReverseMap();
			CreateMap<UpdateUserDto, User>()
				.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<User, UserDto>();
		}
	}
}
