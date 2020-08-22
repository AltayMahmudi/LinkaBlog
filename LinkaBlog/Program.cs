using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LinkaBlog.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LinkaBlog
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			var scope =host.Services.CreateScope();

			var context = scope.ServiceProvider.GetRequiredService < LinkaBlogDbContext>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			context.Database.EnsureCreated();

			var adminRole = new IdentityRole("Admin");



			if (!context.Roles.Any())
			{
				roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
				//Create Role
			}
			if (!context.Users.Any(u=>u.UserName=="admin"))
			{
				var adminUser = new IdentityUser
				{
					UserName = "admin",
					Email = "Admin@gmail.com"
				};
				//Create Admin
				var result= userManager.CreateAsync(adminUser,"password").GetAwaiter().GetResult();

				//Add Role to User
				userManager.AddToRoleAsync(adminUser,adminRole.Name).GetAwaiter().GetResult();
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
