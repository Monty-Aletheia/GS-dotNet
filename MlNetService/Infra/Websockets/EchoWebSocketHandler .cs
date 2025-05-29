using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;

namespace MlNetService.Infra.Websockets
{
	public class EchoWebSocketHandler : WebSocketHandler
	{
		public override async Task HandleAsync(HttpContext context, WebSocket webSocket)
		{
			await EchoLoop(webSocket);
		}
	}
}
