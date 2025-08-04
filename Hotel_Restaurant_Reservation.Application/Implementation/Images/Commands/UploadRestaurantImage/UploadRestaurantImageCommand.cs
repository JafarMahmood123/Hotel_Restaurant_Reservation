using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage
{
    /// <summary>
    /// Represents the command for uploading a restaurant image.
    /// This DTO now directly carries the IFormFile.
    /// </summary>
    public class UploadRestaurantImageCommand : ICommand<Result<string>>
    {
        /// <summary>
        /// The ID of the restaurant the image belongs to.
        /// </summary>
        public Guid RestaurantId { get; set; }

        /// <summary>
        /// The image file being uploaded.
        /// </summary>
        public IFormFile ImageFile { get; set; }
    }
}
