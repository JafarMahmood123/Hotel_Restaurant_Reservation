using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRoomImages
{
    /// <summary>
    /// Handles the GetRoomImagesQuery.
    /// </summary>
    public class GetRoomImagesQueryHandler : IQueryHandler<GetRoomImagesQuery, Result<List<string>>>
    {
        private readonly IGenericRepository<RoomImage> _roomImageRepository;
        private readonly IGenericRepository<Room> _roomRepository;

        public GetRoomImagesQueryHandler(IGenericRepository<RoomImage> roomImageRepository, IGenericRepository<Room> roomRepository)
        {
            _roomImageRepository = roomImageRepository;
            _roomRepository = roomRepository;
        }

        /// <summary>
        /// Handles the request to fetch all image URLs for a given room.
        /// </summary>
        public async Task<Result<List<string>>> Handle(GetRoomImagesQuery request, CancellationToken cancellationToken)
        {
            // First, check if the room exists.
            var roomExists = await _roomRepository.GetFirstOrDefaultAsync(r => r.Id == request.RoomId);
            if (roomExists == null)
            {
                // If not, return a failure result.
                return Result.Failure<List<string>>(new Error("Room.NotFound", $"The room with ID {request.RoomId} was not found."));
            }

            // Fetch all image URLs for the given room ID.
            var imageUrls = await _roomImageRepository
                .Where(img => img.RoomId == request.RoomId)
                .Select(img => img.ImageUrl)
                .ToListAsync(cancellationToken);

            // Return a success result with the list of URLs.
            // If no images are found, this will correctly return an empty list.
            return Result.Success(imageUrls);
        }
    }
}