using System.ComponentModel.DataAnnotations;

namespace UserService.App.Dtos.User
{
	public class UpdateUserDto
	{
		[StringLength(100, ErrorMessage = "Name must be at most 100 characters long.")]
		public string? Name { get; set; }

		[EmailAddress(ErrorMessage = "The email format is invalid.")]
		public string? Email { get; set; }

		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters long.")]
		public string? Password { get; set; }
	}
}