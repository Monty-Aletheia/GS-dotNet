using MassTransit;
using MlNetService.App.Dtos.Messaging;
using MlNetService.App.Services;

namespace MlNetService.Infra.Messaging.Consumer
{
	public class CreateMarkerInfoConsumer : IConsumer<CreateMarkersResponse>
	{
		private readonly MarkerInfoService _markerInfoService;

		public CreateMarkerInfoConsumer(MarkerInfoService markerInfoService)
		{
			_markerInfoService = markerInfoService;
		}

		public async Task Consume(ConsumeContext<CreateMarkersResponse> context)
		{
			var markerInfo = context.Message.MarkerInfo;

			await _markerInfoService.UpsertMarkerInfoAsync(markerInfo);
		}

	}
}
