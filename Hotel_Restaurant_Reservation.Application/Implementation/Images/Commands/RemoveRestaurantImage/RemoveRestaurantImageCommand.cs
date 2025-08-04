using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveRestaurantImage
{
    /// <summary>
    /// Represents the command for removing a restaurant image by its URL.
    /// </summary>
    public class RemoveRestaurantImageCommand : ICommand<Result>
    {
        public RemoveRestaurantImageCommand(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// The public URL of the image to be removed.
        /// </summary>
        public string ImageUrl { get; }
    }
}
