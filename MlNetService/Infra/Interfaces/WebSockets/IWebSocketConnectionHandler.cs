using System.Net.WebSockets;

namespace MlNetService.Infra.Interfaces.WebSockets
{

	public interface IWebSocketConnectionHandler
	{
		Task HandleAsync(WebSocket socket, CancellationToken stoppingToken);
	}

}