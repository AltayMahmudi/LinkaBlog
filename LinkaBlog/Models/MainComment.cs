﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.Models
{
	public class MainComment:Comment
	{
		public List<SubComment> SubComments { get; set; }
	}
}
