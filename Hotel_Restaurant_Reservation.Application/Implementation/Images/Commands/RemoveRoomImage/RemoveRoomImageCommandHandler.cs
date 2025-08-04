using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveRoomImage
{
    /// <summary>
    /// Handles the execution of the RemoveRoomImageCommand.
    /// </summary>
    public class RemoveRoomImageCommandHandler : ICommandHandler<RemoveRoomImageCommand, Result>
    {
        private readonly IGenericRepository<RoomImage> _roomImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public RemoveRoomImageCommandHandler(
            IGenericRepository<RoomImage> roomImageRepository,
            IFileStorageService fileStorageService)
        {
            _roomImageRepository = roomImageRepository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Handles the command to remove a room image by its URL.
        /// </summary>
        public async Task<Result> Handle(RemoveRoomImageCommand request, CancellationToken cancellationToken)
        {
            // Find the image by its URL.
            var roomImage = await _roomImageRepository
                .GetFirstOrDefaultAsync(ri => ri.ImageUrl == request.ImageUrl);

            if (roomImage is null)
            {
                return Result.Failure(new Error("Image.NotFound", $"The image with URL '{request.ImageUrl}' was not found."));
            }

            // First, delete the actual file from storage.
            await _fileStorageService.DeleteFileAsync(request.ImageUrl);

            // Second, remove the image's record from the database.
            await _roomImageRepository.RemoveAsync(roomImage.Id);

            // Finally, save the changes.
            await _roomImageRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}