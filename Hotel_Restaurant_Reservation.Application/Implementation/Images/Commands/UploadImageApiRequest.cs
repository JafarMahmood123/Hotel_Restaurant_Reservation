using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Restaurant_Reservation.Presentation.ApiModels
{
    /// <summary>
    /// Represents the request model for uploading a single file.
    /// Using a dedicated model is a more robust way to handle file uploads with Swagger.
    /// </summary>
    public class UploadImageApiRequest
    {
        /// <summary>
        /// The file to be uploaded.
        /// </summary>
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
