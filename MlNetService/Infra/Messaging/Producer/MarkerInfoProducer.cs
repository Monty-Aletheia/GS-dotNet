using MassTransit;

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
			Console.WriteLine($"Mensagem enviada para a fila Java: {message}");

		}
	}
}
