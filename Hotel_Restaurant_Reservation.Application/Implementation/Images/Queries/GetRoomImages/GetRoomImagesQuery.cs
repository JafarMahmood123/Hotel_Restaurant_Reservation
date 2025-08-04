using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRoomImages
{
    /// <summary>
    /// Represents the query to get all image URLs for a specific room.
    /// </summary>
    public class GetRoomImagesQuery : IQuery<Result<List<string>>>
    {
        public GetRoomImagesQuery(Guid roomId)
        {
            RoomId = roomId;
        }

        public Guid RoomId { get; }
    }
}