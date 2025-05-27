using Microsoft.EntityFrameworkCore;
using UserService.App.Services;
using UserService.Infra.Data;
using UserService.Infra.Repositories;

public class Startup
{
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<SqlServerContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("SqlServerDB")));

        // Repositories
		services.AddScoped<IUserRepository, UserRepository>();

        // Services
        services.AddScoped<IUserAppService, UserAppService>();

        // Add controllers
        services.AddControllers();

		// Add Swagger (se quiser)
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		// Add outras dependências aqui...
	}

	public void Configure(IApplicationBuilder app, IHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseRouting();

		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}