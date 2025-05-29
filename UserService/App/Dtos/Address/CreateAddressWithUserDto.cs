using System.ComponentModel.DataAnnotations;

namespace UserService.App.Dtos.Address
{

	public class CreateAddressWithUserDto
	{
		[Required(ErrorMessage = "Street is required.")]
		[StringLength(100, ErrorMessage = "Street must be at most 100 characters.")]
		public string Street { get; set; }

		[Required(ErrorMessage = "Number is required.")]
		[StringLength(20, ErrorMessage = "Number must be at most 20 characters.")]
		public string Number { get; set; }

		[Required(ErrorMessage = "Neighborhood is required.")]
		[StringLength(100, ErrorMessage = "Neighborhood must be at most 100 characters.")]
		public string Neighborhood { get; set; }

		[Required(ErrorMessage = "City is required.")]
		[StringLength(100, ErrorMessage = "City must be at most 100 characters.")]
		public string City { get; set; }

		[Required(ErrorMessage = "State is required.")]
		[StringLength(2, MinimumLength = 2, ErrorMessage = "State must be exactly 2 characters.")]
		public string State { get; set; }
	}

}