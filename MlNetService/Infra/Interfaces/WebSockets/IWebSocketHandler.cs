using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;

namespace MlNetService.Infra.Interfaces.WebSockets
{
	
		public interface IWebSocketHandler
		{
			Task HandleAsync(HttpContext context, WebSocket webSocket);
		}

}
