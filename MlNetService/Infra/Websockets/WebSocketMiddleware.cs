using Microsoft.AspNetCore.Http;
using MlNetService.Infra.Interfaces.WebSockets;

namespace MlNetService.Infra.Websockets
{
	public class WebSocketMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IWebSocketHandler _handler;

		public WebSocketMiddleware(RequestDelegate next, IWebSocketHandler handler)
		{
			_next = next;
			_handler = handler;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await _next(context);
				return;
			}

			using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
			await _handler.HandleAsync(context, webSocket);
		}
	}
}
