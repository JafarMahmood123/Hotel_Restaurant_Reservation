using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Infrastructure.Storage
{
    /// <summary>
    /// Implements the IFileStorageService to save and delete files from the local web root folder.
    /// </summary>
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string IMAGES_FOLDER = "images";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Saves the provided IFormFile to a specified subfolder within wwwroot/images.
        /// </summary>
        public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
        {
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, IMAGES_FOLDER, subfolder);

            Directory.CreateDirectory(uploadPath); // Ensure the directory exists

            // Create a unique filename to avoid conflicts
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Copy the file to the target path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the public-facing URL
            return $"/{IMAGES_FOLDER}/{subfolder}/{fileName}";
        }

        /// <summary>
        /// Deletes the specified file asynchronously from the wwwroot folder.
        /// </summary>
        public Task DeleteFileAsync(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return Task.CompletedTask;
            }

            // The relative path might start with a '/', which needs to be removed for Path.Combine.
            var pathToDelete = relativePath.TrimStart('/');
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, pathToDelete);

            // Wrap the synchronous file operation in a Task to match the async interface
            return Task.Run(() =>
            {
                // Check if the file exists before attempting to delete it.
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            });
        }
    }
}
