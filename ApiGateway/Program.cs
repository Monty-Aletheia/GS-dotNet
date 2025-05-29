using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var template = File.ReadAllText("./Config/Ocelot.template.json");
var ocelotJson = template
	.Replace("${USERSERVICE_HOST}", Environment.GetEnvironmentVariable("USERSERVICE_HOST") ?? "localhost")
	.Replace("${USERSERVICE_PORT}", Environment.GetEnvironmentVariable("USERSERVICE_PORT") ?? "8080");

File.WriteAllText("./Config/Ocelot.generated.json", ocelotJson);

builder.Configuration.AddJsonFile("./Config/Ocelot.generated.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

var app = builder.Build();

app.UseCors("AllowAll");

await app.UseOcelot();

app.Run();