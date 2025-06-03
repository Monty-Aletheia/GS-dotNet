using MlNetService.Infra.Config;

var builder = Host.CreateApplicationBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var host = builder.Build();

//await SendTestMessageAsync(host.Services);

host.Run();

//async Task SendTestMessageAsync(IServiceProvider services)
//{
//	using var scope = services.CreateScope();
//	var producer = scope.ServiceProvider.GetRequiredService<MarkerInfoProducer>();
//	var message = new MarkerInfoMessage { Test = "valor" };
//	await producer.SendMarkerInfoAsync(message);
//	Console.WriteLine("Mensagem enviada para as duas filas!");
//}