using MassTransit;
using Microsoft.Extensions.Options;
using MlNetService.App.Services;
using MlNetService.Domain.Interfaces;
using MlNetService.Infra.Interfaces.WebSockets;
using MlNetService.Infra.Messaging.Consumer;
using MlNetService.Infra.Messaging.Producer;
using MlNetService.Infra.Websockets;
using MlNetService.Infra.Worker;
using MongoDB.Driver;

namespace MlNetService.Infra.Config
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<RabbitMqSettings>(Configuration.GetSection("RabbitMQ"));
			services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));

			services.AddSingleton<IMongoClient>(
				s => new MongoClient(Configuration.GetSection("MongoDBSettings")["ConnectionString"]));

			// WebSocket e serviços relacionados (apenas Scoped)
			services.AddHostedService<WebSocketServer>();
			services.AddScoped<IWebSocketConnectionHandler, WebSocketConnectionHandler>();
			services.AddScoped<IMessageProcessor, MessageProcessor>();
			services.AddScoped<MarkerInfoService>();
			services.AddScoped<MarkerInfoProducer>();

			// Outros serviços
			services.AddHttpClient<IGeocodingService, GeocodingService>();
			services.AddSingleton<MlNetAppService>();

			// MassTransit
			services.AddMassTransit(x =>
			{
				x.AddConsumer<GetMarkersConsumer>();

				x.AddConsumer<CreateMarkerInfoConsumer>();

				x.UsingRabbitMq((context, cfg) =>
				{
					var rabbitMqSettings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

					cfg.Host(rabbitMqSettings.Host, "/", h =>
					{
						h.Username(rabbitMqSettings.Username);
						h.Password(rabbitMqSettings.Password);
					});

					cfg.ReceiveEndpoint("java-queue", ep => { });

					cfg.ReceiveEndpoint("mobile-queue", ep => { });


					cfg.ReceiveEndpoint("get-markers-request-queue", e =>
					{
						e.UseRawJsonSerializer();
						e.ConfigureConsumer<GetMarkersConsumer>(context);
					});

					cfg.ReceiveEndpoint("create-marker-info", e =>
					{
						e.UseRawJsonSerializer();
						e.ConfigureConsumer<CreateMarkerInfoConsumer>(context);
					});
				});
			});
		}

	}
}