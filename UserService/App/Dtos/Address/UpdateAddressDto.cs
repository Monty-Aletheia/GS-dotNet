using System.ComponentModel.DataAnnotations;

namespace UserService.App.Dtos.Address
{
	public class UpdateAddressDto
	{
		[Required]
		public Guid Id { get; set; }

		[StringLength(100)]
		public string Street { get; set; }

		[StringLength(20)]
		public string Number { get; set; }

		[StringLength(100)]
		public string Neighborhood { get; set; }

		[StringLength(100)]
		public string City { get; set; }

		[StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters.")]
		public string State { get; set; }
	}
}