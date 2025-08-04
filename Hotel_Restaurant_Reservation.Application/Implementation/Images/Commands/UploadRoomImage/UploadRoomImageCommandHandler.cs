using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRoomImage
{
    /// <summary>
    /// Handles the UploadRoomImageCommand.
    /// </summary>
    public class UploadRoomImageCommandHandler : ICommandHandler<UploadRoomImageCommand, Result<string>>
    {
        private readonly IGenericRepository<Room> _roomRepository;
        private readonly IGenericRepository<RoomImage> _roomImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public UploadRoomImageCommandHandler(
            IGenericRepository<Room> roomRepository,
            IGenericRepository<RoomImage> roomImageRepository,
            IFileStorageService fileStorageService)
        {
            _roomRepository = roomRepository;
            _roomImageRepository = roomImageRepository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Handles the request to upload an image for a given room.
        /// </summary>
        public async Task<Result<string>> Handle(UploadRoomImageCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetFirstOrDefaultAsync(r => r.Id == request.RoomId);
            if (room is null)
            {
                return Result.Failure<string>(new Error("Room.NotFound", $"The room with ID {request.RoomId} was not found."));
            }

            if (request.UploadImageApiRequest.ImageFile == null)
            {
                return Result.Failure<string>(new Error("Room.NoImageProvided", "No image file was provided."));
            }

            const string SUBFOLDER_NAME = "rooms";

            var imageUrl = await _fileStorageService.SaveFileAsync(request.UploadImageApiRequest.ImageFile, SUBFOLDER_NAME);

            var roomImage = new RoomImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = imageUrl,
                RoomId = request.RoomId
            };

            await _roomImageRepository.AddAsync(roomImage);

            await _roomRepository.SaveChangesAsync();

            return Result.Success(imageUrl);
        }
    }
}