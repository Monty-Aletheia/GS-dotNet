using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Models
{
	[Table("tb_devices")]
	public class Device
	{
		[Key]
		[Column("id")]
		public Guid Id { get; set; }

		[Required]
		[Column("device_name")]
		public string DeviceName { get; set; }

		[Required]
		[Column("expo_device_token")]
		public string ExpoDeviceToken { get; set; }

		[Required]
		[Column("user_id")]
		public Guid UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public virtual User User { get; set; }


	}
}