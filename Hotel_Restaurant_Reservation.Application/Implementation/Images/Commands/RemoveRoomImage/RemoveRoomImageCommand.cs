using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveRoomImage
{
    /// <summary>
    /// Represents the command to remove a single room image by its URL.
    /// </summary>
    public class RemoveRoomImageCommand : ICommand<Result>
    {
        public RemoveRoomImageCommand(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// The URL of the image to remove.
        /// </summary>
        public string ImageUrl { get; }
    }
}