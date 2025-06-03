using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MlNetService.App.Services;
using MlNetService.Domain.Interfaces;
using MlNetService.Infra.Messaging.Consumer;
using MlNetService.Infra.Messaging.Producer;
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

			services.AddHostedService<WebSocketServer>();

			services.AddHttpClient<IGeocodingService, GeocodingService>();
			services.AddTransient<MarkerInfoProducer>();

			services.AddMassTransit(x =>
			{
				x.AddConsumer<IoTMessageConsumer>();

				x.UsingRabbitMq((context, cfg) =>
				{
					var rabbitMqSettings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

					cfg.Host(rabbitMqSettings.Host, "/", h =>
					{
						h.Username(rabbitMqSettings.Username);
						h.Password(rabbitMqSettings.Password);
					});

					cfg.ReceiveEndpoint("java-queue", ep =>
					{
					});
					cfg.ReceiveEndpoint("mobile-queue", ep =>
					{
					});
				});
			});
		}

	}
}