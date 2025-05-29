using MlNetService.Infra.Config;

var builder = Host.CreateApplicationBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var host = builder.Build();

host.Run();