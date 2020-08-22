using LinkaBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.Data
{
	public class LinkaBlogDbContext : IdentityDbContext
	{
		public LinkaBlogDbContext(DbContextOptions<LinkaBlogDbContext>options) :base(options)
		{
			Database.EnsureCreated();
		}
		public DbSet<Post> Posts { get; set; }
		public DbSet<MainComment> MainComments { get; set; }
		public DbSet<SubComment> SubComments { get; set; }

	}
}
