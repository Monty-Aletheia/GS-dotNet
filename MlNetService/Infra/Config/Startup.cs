using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MlNetService.Infra.Messaging.Consumer;
using MlNetService.Infra.Worker;

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

			services.AddHostedService<WebSocketServer>();


			//services.AddMassTransit(x =>
			//{
			//	x.AddConsumer<IoTMessageConsumer>();

			//	x.UsingRabbitMq((context, cfg) =>
			//	{
			//		var rabbitMqSettings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

			//		cfg.Host(rabbitMqSettings.Host, "/", h => {
			//			h.Username(rabbitMqSettings.Username);
			//			h.Password(rabbitMqSettings.Password);
			//		});

			//		cfg.ReceiveEndpoint(rabbitMqSettings.Queue, e =>
			//		{
			//			e.ConfigureConsumer<IoTMessageConsumer>(context);
			//		});
			//	});
			//});
		}

	}
}
