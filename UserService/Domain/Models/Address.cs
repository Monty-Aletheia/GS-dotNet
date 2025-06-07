using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Models
{
	[Table("tb_address")]
	public class Address
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		[Column("street")]
		public string Street { get; set; }

		[Required]
		[Column("number")]
		public string Number { get; set; }

		[Required]
		[Column("neighborhood")]
		public string Neighborhood { get; set; }

		[Required]
		[Column("city")]
		public string City { get; set; }

		[Required]
		[MaxLength(2)]
		[Column("state")]
		public string State { get; set; }

		[Required]
		[Column("user_id")]
		public Guid UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}