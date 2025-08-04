using System.ComponentModel.DataAnnotations;

namespace Hotel_Restaurant_Reservation.Presentation.ApiModels
{
    /// <summary>
    /// Represents the request model for removing an image by its URL.
    /// </summary>
    public class RemoveImageApiRequest
    {
        /// <summary>
        /// The URL of the image to be deleted.
        /// </summary>
        [Required]
        public string ImageUrl { get; set; }
    }
}
