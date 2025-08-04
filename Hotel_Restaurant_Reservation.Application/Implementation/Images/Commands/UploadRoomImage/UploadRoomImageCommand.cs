using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Hotel_Restaurant_Reservation.Presentation.ApiModels;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRoomImage
{
    /// <summary>
    /// Represents the command to upload a single image for a room.
    /// </summary>
    public class UploadRoomImageCommand : ICommand<Result<string>>
    {
        public UploadRoomImageCommand(Guid roomId, UploadImageApiRequest uploadImageApiRequest)
        {
            RoomId = roomId;
            UploadImageApiRequest = uploadImageApiRequest;
        }

        public Guid RoomId { get; set; }
        public UploadImageApiRequest UploadImageApiRequest { get; set; }
    }
}