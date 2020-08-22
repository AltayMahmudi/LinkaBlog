using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkaBlog.Models;
using LinkaBlog.Data.Repository;
using LinkaBlog.Data.File_Manager;

namespace LinkaBlog.Controllers
{
	public class HomeController : Controller
	{
		private IRepository _context;
		private IFileManager _fileManager;


		public HomeController(IRepository context, IFileManager fileManager)
		{
			_context = context;
			_fileManager = fileManager;
		}
		public IActionResult Index(string subcategory)
		{

			var posts = string.IsNullOrEmpty(subcategory) ? _context.GetAllPosts() : _context.GetAllPostsSub(subcategory);
			return View(posts);
		}

	}
}
