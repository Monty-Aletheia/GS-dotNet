using MassTransit;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace MlNetService.Infra.Messaging.Producer
{
	public class MarkerInfoProducer 
	{
		private readonly ISendEndpointProvider _sendEndpointProvider;

		public MarkerInfoProducer(ISendEndpointProvider sendEndpointProvider)
		{
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task SendMarkerInfoAsync(object message)
		{
			var endpointJava = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:java-queue"));
			await endpointJava.Send(message);

			var endpointMobile = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:mobile-queue"));
			await endpointMobile.Send(message);
		}
	}
}
