using System.ComponentModel.DataAnnotations;

namespace UserService.App.Dtos.Device
{
	public class CreateDeviceDto
	{
		[Required(ErrorMessage = "Device name is required.")]
		[StringLength(100, ErrorMessage = "Device name must be at most 100 characters.")]
		public string DeviceName { get; set; }

		[Required(ErrorMessage = "Expo device token is required.")]
		[StringLength(200, ErrorMessage = "Expo device token must be at most 200 characters.")]
		public string ExpoDeviceToken { get; set; }

		[Required(ErrorMessage = "User ID is required.")]
		public Guid UserId { get; set; }
	}
}