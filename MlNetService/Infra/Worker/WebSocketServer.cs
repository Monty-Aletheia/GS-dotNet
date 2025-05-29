using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MlNetService.Infra.Worker
{
	public class WebSocketServer : BackgroundService
	{
		private readonly ILogger<WebSocketServer> _logger;

		public WebSocketServer(ILogger<WebSocketServer> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var server = new HttpListener();
			// Altere para aceitar qualquer origem
			server.Prefixes.Add("http://+:9090/ws/");
			server.Start();

			_logger.LogInformation("WebSocket Server started...");

			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation("Aguardando conexão...");

				var context = await server.GetContextAsync();

				// Adicione headers CORS
				context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
				context.Response.Headers.Add("Access-Control-Allow-Headers", "*");

				_logger.LogInformation("Requisição recebida: {Method} {Url}", context.Request.HttpMethod, context.Request.Url);
				_logger.LogInformation("Headers: {Headers}", string.Join(", ", context.Request.Headers.AllKeys.Select(k => $"{k}: {context.Request.Headers[k]}")));

				if (context.Request.IsWebSocketRequest)
				{
					_logger.LogInformation("Requisição WebSocket detectada. Aceitando conexão...");
					var wsContext = await context.AcceptWebSocketAsync(null);
					_ = HandleWebSocket(wsContext.WebSocket);
				}
				else
				{
					_logger.LogWarning("Requisição não é WebSocket, retornando 400.");
					context.Response.StatusCode = 400;
					context.Response.Close();
				}
			}

			server.Stop();
		}


		private async Task HandleWebSocket(System.Net.WebSockets.WebSocket socket)
		{
			_logger.LogInformation("WebSocket conectado.");

			var buffer = new byte[1024 * 4];
			var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

			while (!result.CloseStatus.HasValue)
			{
				var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
				_logger.LogInformation("Mensagem recebida: {msg}", msg);

				await socket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
				result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			}

			_logger.LogInformation("WebSocket desconectado. Status: {status} - {desc}", result.CloseStatus, result.CloseStatusDescription);

			await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
		}

	}

}