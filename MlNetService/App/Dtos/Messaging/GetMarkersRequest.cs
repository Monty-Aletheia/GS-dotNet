namespace MlNetService.App.Dtos.Messaging
{
	public class GetMarkersRequest
	{
		public string RequestId { get; set; } = Guid.NewGuid().ToString();
	}
}