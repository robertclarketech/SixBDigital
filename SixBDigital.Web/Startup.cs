namespace SixBDigital.Web
{
	using System;
	using System.Linq;
	using System.Reflection;
	using MediatR;
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Serilog;
	using SixBDigital.Domain.Builders;
	using SixBDigital.Domain.Builders.Interfaces;
	using SixBDigital.Domain.Commands;
	using SixBDigital.Domain.Entities;
	using SixBDigital.Infrastructure.CommandHandlers.Generic;
	using SixBDigital.Infrastructure.EntityFramework;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<SixBDigitalContext>(options =>
				options.UseSqlite(
					Configuration.GetConnectionString("Context"),
					builder => builder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
				)
			);

			services.AddMediatR(AppDomain
				.CurrentDomain
				.GetAssemblies()
				.First(assembly => assembly.GetName().Name == Configuration.GetValue<string>("MediatrAssembly")));

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));

			services.AddTransient<IBuilder, BookingBuilder>();
			services.AddTransient<IRequestHandler<CreateBookingCommand, Unit>, CreateEntityCommandHandler<CreateBookingCommand, Booking>>();

			services.AddControllersWithViews().AddRazorRuntimeCompilation();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseSerilogRequestLogging();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCookiePolicy();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
