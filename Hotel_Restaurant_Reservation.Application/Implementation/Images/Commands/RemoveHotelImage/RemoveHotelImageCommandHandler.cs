using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveHotelImage
{
    /// <summary>
    /// Handles the execution of the RemoveHotelImageCommand.
    /// </summary>
    public class RemoveHotelImageCommandHandler : ICommandHandler<RemoveHotelImageCommand, Result>
    {
        private readonly IGenericRepository<HotelImage> _hotelImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public RemoveHotelImageCommandHandler(
            IGenericRepository<HotelImage> hotelImageRepository,
            IFileStorageService fileStorageService)
        {
            _hotelImageRepository = hotelImageRepository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Handles the command to remove a hotel image by its URL.
        /// </summary>
        public async Task<Result> Handle(RemoveHotelImageCommand request, CancellationToken cancellationToken)
        {
            // Find the image by its URL, ensuring it belongs to the specified hotel.
            var hotelImage = await _hotelImageRepository
                .GetFirstOrDefaultAsync(hi => hi.Url == request.ImageUrl);

            if (hotelImage is null)
            {
                return Result.Failure(new Error("Image.NotFound", $"The image with URL '{request.ImageUrl}' was not found."));
            }

            // First, delete the actual file from storage using the provided URL.
            await _fileStorageService.DeleteFileAsync(request.ImageUrl);

            // Second, remove the image's record from the database.
            await _hotelImageRepository.RemoveAsync(hotelImage.Id);

            // Finally, save the changes.
            await _hotelImageRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}