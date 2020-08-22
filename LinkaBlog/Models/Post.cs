using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public string Body { get; set; } = "";
		public string AdminName { get; set; } = "";
		public string Image { get; set; } = "";
		public string Description { get; set; } = "";
		public string Tags { get; set; } = "";
		public string SubCategory { get; set; } = "";
		public string IsActive { get; set; } = "";
		public string Category { get; set; } = "";

		public List<MainComment> MainComments { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;

	}
}
