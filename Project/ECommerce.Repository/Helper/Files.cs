using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Helper
{
	public static class Files
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			// 1. get located folder
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", folderName);
			// 2. get file name and make it unique
			string fileName = $"{Guid.NewGuid()}{file.FileName}";
			// 3. get the file paht[folder path + fileName]
			string filePath = Path.Combine(folderPath, fileName);
			// 4. save file as stream
			using var fileStream = new FileStream(filePath, FileMode.Create);
			file.CopyTo(fileStream);
			// return file name
			return fileName;

		}
		public static void DeleteFile(string fileName, string folderName)
		{
			// 1. get file path
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", folderName, fileName);
			// 2. check if it not exist
			if (File.Exists(filePath))
				File.Delete(filePath);
		}
	}
}
