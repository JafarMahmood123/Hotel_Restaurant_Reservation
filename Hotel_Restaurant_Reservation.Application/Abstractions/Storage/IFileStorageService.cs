using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Storage
{
    /// <summary>
    /// Defines the contract for a service that handles file storage and deletion.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Saves a file asynchronously.
        /// </summary>
        /// <param name="file">The file to save, as an IFormFile.</param>
        /// <param name="subfolder">The subfolder within the storage root to save the file.</param>
        /// <returns>The public URL path of the saved file.</returns>
        Task<string> SaveFileAsync(IFormFile file, string subfolder);

        /// <summary>
        /// Deletes a file asynchronously from storage.
        /// </summary>
        /// <param name="relativePath">The web-relative path to the file (e.g., "/images/restaurants/file.jpg").</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteFileAsync(string relativePath);
    }
}
