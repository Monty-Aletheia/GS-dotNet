using MassTransit;
using MlNetService.App.Dtos.Messaging;
using MlNetService.App.Services;

namespace MlNetService.Infra.Messaging.Consumer
{
	public class GetMarkersConsumer : IConsumer<GetMarkersRequest>
	{
		private readonly MarkerInfoService _markerInfoService;

		public GetMarkersConsumer(MarkerInfoService markerInfoService)
		{
			_markerInfoService = markerInfoService;
		}

		public async Task Consume(ConsumeContext<GetMarkersRequest> context)
		{
			var markers = _markerInfoService.Get();

			await context.RespondAsync(new GetMarkersResponse
			{
				Markers = markers
			});

		}
	}

}