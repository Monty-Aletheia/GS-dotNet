using Microsoft.EntityFrameworkCore;
using UserService.App.Services;
using UserService.App.Services.Mappers;
using UserService.Domain.Interfaces.Repositories;
using UserService.Domain.Interfaces.Services;
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
		services.AddScoped<IAddressRepository, AddressRepository>();

		// Services
		services.AddAutoMapper(typeof(UserProfile), typeof(AddressProfile));

		services.AddScoped<IUserAppService, UserAppService>();
		services.AddScoped<IAddressService, AddressService>();

		// Add controllers
		services.AddControllers();

		// Add Swagger 
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

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