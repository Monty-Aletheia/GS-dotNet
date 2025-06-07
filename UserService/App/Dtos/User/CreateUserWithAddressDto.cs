using System.ComponentModel.DataAnnotations;
using UserService.App.Dtos.Address;

namespace UserService.App.Dtos.User
{
	public class CreateUserWithAddressDto
	{
		[Required(ErrorMessage = "Name is required.")]
		[StringLength(100, ErrorMessage = "Name must be at most 100 characters long.")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "The email format is invalid.")]
		public string Email { get; set; } = null!;

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters long.")]
		public string Password { get; set; } = null!;

		public string? FirebaseId { get; set; }


		[Required(ErrorMessage = "Address is required.")]
		public CreateAddressWithUserDto Address { get; set; } = null!;
	}
}