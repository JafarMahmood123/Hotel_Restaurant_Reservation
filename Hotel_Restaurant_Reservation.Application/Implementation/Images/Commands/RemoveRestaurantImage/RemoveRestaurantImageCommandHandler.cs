using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.RemoveRestaurantImage
{
    /// <summary>
    /// Handles the RemoveRestaurantImageCommand.
    /// </summary>
    public class RemoveRestaurantImageCommandHandler : ICommandHandler<RemoveRestaurantImageCommand, Result>
    {
        private readonly IGenericRepository<RestaurantImage> _restaurantImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public RemoveRestaurantImageCommandHandler(
            IGenericRepository<RestaurantImage> restaurantImageRepository,
            IFileStorageService fileStorageService)
        {
            _restaurantImageRepository = restaurantImageRepository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Handles the command to delete an image record from the database and the corresponding file from storage.
        /// </summary>
        public async Task<Result> Handle(RemoveRestaurantImageCommand request, CancellationToken cancellationToken)
        {
            // Find the image record in the database by its URL.
            var image = await _restaurantImageRepository
                .GetFirstOrDefaultAsync(img => img.Url == request.ImageUrl);

            if (image is null)
            {
                return Result.Failure(new Error("Image.NotFound", $"The image with URL '{request.ImageUrl}' was not found."));
            }

            // Delete the physical file from the server.
            await _fileStorageService.DeleteFileAsync(image.Url);

            // Remove the image record from the database.
            await _restaurantImageRepository.RemoveAsync(image.Id);

            // Save the changes.
            await _restaurantImageRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
