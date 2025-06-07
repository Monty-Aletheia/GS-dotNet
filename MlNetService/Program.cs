using MassTransit;
using MlNetService.App.Dtos.Messaging;
using MlNetService.Infra.Config;

var builder = Host.CreateApplicationBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var host = builder.Build();

if (args.Contains("train"))
{
	var mlService = host.Services.GetRequiredService<MlNetAppService>();
	mlService.TrainModel();
}


host.Run();