using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantDishImage
{
    /// <summary>
    /// Represents the command for uploading an image for a specific restaurant dish.
    /// </summary>
    public class UploadRestaurantDishImageCommand : ICommand<Result<string>>
    {
        /// <summary>
        /// The ID of the restaurant the dish belongs to.
        /// </summary>
        public Guid RestaurantId { get; set; }

        /// <summary>
        /// The ID of the dish to associate the image with.
        /// </summary>
        public Guid DishId { get; set; }

        /// <summary>
        /// The image file being uploaded.
        /// </summary>
        public IFormFile ImageFile { get; set; }
    }
}
