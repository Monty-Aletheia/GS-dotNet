using MlNetService.Infra.Interfaces.WebSockets;
using System.Net;

namespace MlNetService.Infra.Worker
{
	public class WebSocketServer : BackgroundService
	{
		private readonly ILogger<WebSocketServer> _logger;
		private readonly IServiceScopeFactory _scopeFactory;

		public WebSocketServer(
			ILogger<WebSocketServer> logger,
			IServiceScopeFactory scopeFactory)
		{
			_logger = logger;
			_scopeFactory = scopeFactory;
		}


		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var server = new HttpListener();
			server.Prefixes.Add("http://localhost:9090/ws/");
			server.Start();

			_logger.LogInformation("WebSocket Server started...");

			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation("Aguardando conexão...");
				var context = await server.GetContextAsync();

				context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
				context.Response.Headers.Add("Access-Control-Allow-Headers", "*");

				_logger.LogInformation("Requisição recebida: {Method} {Url}", context.Request.HttpMethod, context.Request.Url);

				if (context.Request.IsWebSocketRequest)
				{
					_logger.LogInformation("Requisição WebSocket detectada. Aceitando conexão...");
					var wsContext = await context.AcceptWebSocketAsync(null);

					_ = Task.Run(async () =>
					{
						using var scope = _scopeFactory.CreateScope();
						var handler = scope.ServiceProvider.GetRequiredService<IWebSocketConnectionHandler>();
						await handler.HandleAsync(wsContext.WebSocket, stoppingToken);
					});
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
	}
}