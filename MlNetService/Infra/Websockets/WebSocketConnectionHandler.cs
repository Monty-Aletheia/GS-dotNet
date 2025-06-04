using MlNetService.Infra.Interfaces.WebSockets;
using MlNetService.Infra.Worker;
using System.Net.WebSockets;
using System.Text;

namespace MlNetService.Infra.Websockets
{
	public class WebSocketConnectionHandler : IWebSocketConnectionHandler
	{
		private readonly ILogger<WebSocketConnectionHandler> _logger;
		private readonly IMessageProcessor _messageProcessor;

		public WebSocketConnectionHandler(
			ILogger<WebSocketConnectionHandler> logger,
			IMessageProcessor messageProcessor)
		{
			_logger = logger;
			_messageProcessor = messageProcessor;
		}

		public async Task HandleAsync(WebSocket socket, CancellationToken stoppingToken)
		{
			_logger.LogInformation("WebSocket conectado.");
			var buffer = new byte[4096];

			try
			{
				while (socket.State == WebSocketState.Open && !stoppingToken.IsCancellationRequested)
				{
					var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);

					if (result.MessageType == WebSocketMessageType.Close)
					{
						_logger.LogInformation("WebSocket desconectado. Status: {status} - {desc}", result.CloseStatus, result.CloseStatusDescription);
						await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, stoppingToken);
						break;
					}

					var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
					_logger.LogInformation("Mensagem recebida: {msg}", msg);

					var response = await _messageProcessor.ProcessAsync(msg);
					var responseBytes = Encoding.UTF8.GetBytes(response);
					await socket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, stoppingToken);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro no WebSocket");
			}
			finally
			{
				if (socket.State != WebSocketState.Closed)
				{
					await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Encerrando conexão", stoppingToken);
				}
			}
		}
	}
}