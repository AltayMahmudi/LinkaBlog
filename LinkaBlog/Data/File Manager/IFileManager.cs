using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.Data.File_Manager
{
	public interface IFileManager
	{
		FileStream ImageStream(string image); 
		Task<string> SaveImage(IFormFile image);
		bool RemoveImage(string image);
	}
}
