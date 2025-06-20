﻿using MassTransit;
using MlNetService.Domain.Models;

namespace MlNetService.Infra.Messaging.Producer
{
	public class MarkerInfoProducer
	{
		private readonly ISendEndpointProvider _sendEndpointProvider;

		public MarkerInfoProducer(ISendEndpointProvider sendEndpointProvider)
		{
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task SendMarkerInfoAsync(MarkerInfo message)
		{
			var endpointJava = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:java-queue"));

			var endpointMobile = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:mobile-queue"));

			await endpointJava.Send(message);
		}

		public async Task SendAllMarkersInfosAsync(IEnumerable<object> messages)
		{
			var endpointJava = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:java-queue"));
			foreach (var message in messages)
			{
				await endpointJava.Send(message);
			}
		}
	}
}