using MlNetService.Domain.Models;

namespace MlNetService.App.Dtos.Messaging
{

	public class GetMarkersResponse
	{
		public List<MarkerInfo> Markers { get; set; }
	}
}
