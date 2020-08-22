using LinkaBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.ViewModels
{
	public class IndexViewModel
	{
		public int PageNumber { get; set; }
		public bool NextPage { get; set; }
		public int PageCount { get; set; }
		public string Category { get; set; }
		public IEnumerable<Post> Posts { get; set; }
	}
}
