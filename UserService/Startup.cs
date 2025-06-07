using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
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
		//services.AddDbContext<SqlServerContext>(options =>
		//	options.UseSqlServer(Configuration.GetConnectionString("SqlServerDB")));

		services.AddDbContext<OracleFiapContext>(options =>
			options.UseOracle(Configuration.GetConnectionString("OracleFiap")));
		// Repositories
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IAddressRepository, AddressRepository>();
		services.AddScoped<IDeviceRepository, DeviceRepository>();

		// Services
		services.AddAutoMapper(typeof(UserProfile), typeof(AddressProfile), typeof(DeviceProfile));

		services.AddScoped<IUserAppService, UserAppService>();
		services.AddScoped<IAddressService, AddressService>();
		services.AddScoped<IDeviceService, DeviceService>();

		// Add controllers
		services.AddControllers();

		// Add Swagger 
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddRateLimiter(options =>
		{
			options.AddFixedWindowLimiter("fixed", o =>
			{
				o.Window = TimeSpan.FromMinutes(1);
				o.PermitLimit = 100;
				o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
				o.QueueLimit = 0;
			});
		});

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

		app.UseRateLimiter();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers().RequireRateLimiting("fixed");
		});
	}
}