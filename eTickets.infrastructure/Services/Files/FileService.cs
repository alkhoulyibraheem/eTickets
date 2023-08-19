using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<byte[]> GetFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(_env.WebRootPath, folderName, fileName);
            return await File.ReadAllBytesAsync(filePath);
        }
        
        public async Task<string> GetFileBase64(string folderName, string fileName)
        {
            var file = await GetFile(folderName, fileName);
            return Convert.ToBase64String(file);
        }



		private static readonly Dictionary<string, byte[]> ImageSignatures = new Dictionary<string, byte[]>
		{
			{ ".jpg", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 } }, // JPEG
            { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } }, // PNG
            { ".gif", new byte[] { 0x47, 0x49, 0x46, 0x38 } }, // GIF
            { ".bmp", new byte[] { 0x42, 0x4D } },             // BMP
            { ".tiff", new byte[] { 0x49, 0x49, 0x2A, 0x00 } }, // TIFF (little-endian)
           // { ".tiff", new byte[] { 0x4D, 0x4D, 0x00, 0x2A } }, // TIFF (big-endian)
            // Add more image types and their signatures here
        };

		private static bool IsImageValid(IFormFile imageFile)
		{

			foreach (var validExtension in ImageSignatures.Keys)
			{
				byte[] signature = ImageSignatures[validExtension];
				byte[] buffer = new byte[signature.Length];

				using (var stream = imageFile.OpenReadStream())
				{
					// Read the signature from the image stream
					stream.Read(buffer, 0, signature.Length);
				}

				// Compare the read signature with the expected one
				if (buffer.SequenceEqual(signature))
				{
					return true;
				}
			}

			return false; // No valid extension matched
		}




		public async Task<string> SaveFile(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
				if (file.Length <= 5242880 )
                {
					var uploads = Path.Combine(_env.WebRootPath, folderName);
					fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
					await using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
					await file.CopyToAsync(fileStream);
				}
				else
				{
                    return "1";
				}

			}

            return fileName;
        }

        public async Task<string> SaveFile(byte[] file, string folderName, string extension)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), file);
            }

            return fileName;
        }

        public async Task<string> SaveFile(string file, string folderName, string extension)
        {
            string fileName = null;
            if (!string.IsNullOrWhiteSpace(file))
            {
                file = file.Substring(file.IndexOf(",", StringComparison.Ordinal) + 1);
				var bytes = Convert.FromBase64String(file);
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), bytes);
                
            }

            return fileName;
        }
		public async Task<string> SaveImage(IFormFile file, string folderName)
		{
			var allowedImageContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };

			if (!allowedImageContentTypes.Contains(file.ContentType))
			{
				throw new Exception();
			}
			return await SaveFile(file, folderName);
		}









	}
}
