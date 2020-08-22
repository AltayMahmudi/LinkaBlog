﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace LinkaBlog.Data.File_Manager
{
	public class FileManager:IFileManager
	{
		private string _imagePath;

		public FileManager(IConfiguration config) 
		{
			_imagePath = config["Path:Images"];
		}

		public FileStream ImageStream(string image)
		{
			return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
		}

		public bool RemoveImage(string image)
		{
			var file = Path.Combine(_imagePath, image);
			if (File.Exists(file))
				File.Delete(file);
				   return true;
			
		}

		public async Task<string> SaveImage(IFormFile image)
		{
			var save_path= Path.Combine(_imagePath);
			if (Directory.Exists(save_path)) 
			{
				Directory.CreateDirectory(save_path);
			}
			var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
			var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";
			using (var fileStream = new FileStream(Path.Combine(save_path, fileName),FileMode.Create))
			{
				//await image.CopyToAsync(fileStream);
				MagicImageProcessor.ProcessImage(image.OpenReadStream(),fileStream,ImageOptions());
			}
			
			return fileName;
		}

		private ProcessImageSettings ImageOptions() => new ProcessImageSettings
		{
			Width=1200,
			Height=800,
			ResizeMode=CropScaleMode.Crop,
			SaveFormat=FileFormat.Jpeg,
			JpegQuality=100,
			JpegSubsampleMode=ChromaSubsampleMode.Subsample420

		};
	}
}
