using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkaBlog.Data;
using LinkaBlog.Data.File_Manager;
using LinkaBlog.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkaBlog
{
	public class Startup
	{
		private IConfiguration _config;
		public Startup(IConfiguration config)
		{
			_config = config;
		}


		public IConfiguration Configuration;
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<LinkaBlogDbContext>(options =>

	        options.UseSqlServer(_config.GetConnectionString("Default")));



			services.AddIdentity<IdentityUser ,IdentityRole>(options=>
			{ options.Password.RequireDigit = false; 
			  options.Password.RequireNonAlphanumeric = false; 
			  options.Password.RequireUppercase = false;
			  options.Password.RequiredLength = 5;
			 })
				    //.AddRoles<IdentityRole>()
				    .AddEntityFrameworkStores<LinkaBlogDbContext>();

			services. ConfigureApplicationCookie(options=> { options.LoginPath = "/Auth/Login"; });
			services.AddTransient<IRepository, Repository>();
			services.AddTransient<IFileManager, FileManager>();

			services.AddMvc(options=>options.CacheProfiles.Add("Monthly",new CacheProfile { Duration=60*60*24*7*4})).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));

				options.AddPolicy("ManagerAccess", policy =>
					policy.RequireAssertion(context =>
								context.User.IsInRole("Admin")
								|| context.User.IsInRole("Manager")));

				options.AddPolicy("UserAccess", policy =>
					policy.RequireAssertion(context =>
								context.User.IsInRole("Admin")
								|| context.User.IsInRole("Manager")
								|| context.User.IsInRole("User")));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
