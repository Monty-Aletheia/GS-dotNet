using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Models
{
	[Table("tb_users")]
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		[Column("name")]
		public string Name { get; set; }

		[Required]
		[Column("email")]
		public string Email { get; set; }

		[Required]
		[Column("password")]
		public string Password { get; set; }

		[Column("firebase_id")]
		public string FirebaseId { get; set; }

		public Address Address { get; set; }

		public ICollection<Device>? Devices { get; set; } = new List<Device>();
	}
}