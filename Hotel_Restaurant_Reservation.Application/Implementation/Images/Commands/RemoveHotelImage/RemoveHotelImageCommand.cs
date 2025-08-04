using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveHotelImage
{
    /// <summary>
    /// Represents the command to remove a single hotel image by its URL.
    /// </summary>
    public class RemoveHotelImageCommand : ICommand<Result>
    {
        public RemoveHotelImageCommand(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// The URL of the image to remove.
        /// </summary>
        public string ImageUrl { get; }
    }
}