using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.ViewModels
{
	public class PostViewModel
	{

		public int Id { get; set; }

		public string AdminName { get; set; } = "";

		[Required]
		public string Title { get; set; } = "";
		[Required]
		public string Body { get; set; } = "";
		[Required]
		public string Description { get; set; } = "";
		[Required]
		public string Tags { get; set; } = "";

		[Required]
		public string IsActive { get; set; } = "";

		public string SubCategory { get; set; } = "";

		public string Category { get; set; } = "";
		public string CurrentImage { get; set; } = "";
		public IFormFile Image { get; set; } = null;

		public DateTime Created { get; set; } = DateTime.Now;
	}
}
