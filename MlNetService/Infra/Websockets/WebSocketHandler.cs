using Microsoft.AspNetCore.Http;
using MlNetService.Infra.Interfaces.WebSockets;
using System.Net.WebSockets;

namespace MlNetService.Infra.Websockets
{
	public abstract class WebSocketHandler : IWebSocketHandler
	{
		public abstract Task HandleAsync(HttpContext context, WebSocket webSocket);

		protected async Task EchoLoop(WebSocket webSocket)
		{
			var buffer = new byte[1024 * 4];
			var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

			while (!result.CloseStatus.HasValue)
			{
				await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
				result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			}

			await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
		}
	}
}