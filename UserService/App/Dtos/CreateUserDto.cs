using System.ComponentModel.DataAnnotations;

namespace UserService.App.Dtos
{
	public class CreateUserDto
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
	}
}