using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string Message { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;

	}
}
