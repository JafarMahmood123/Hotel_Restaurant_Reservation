using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage
{
    /// <summary>
    /// Handles the UploadRestaurantImageCommand.
    /// </summary>
    public class UploadRestaurantImageCommandHandler : ICommandHandler<UploadRestaurantImageCommand, Result<string>>
    {
        private readonly IRestaurantRespository _restaurantRepository;
        private readonly IGenericRepository<RestaurantImage> _restaurantImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public UploadRestaurantImageCommandHandler(
            IRestaurantRespository restaurantRepository,
            IGenericRepository<RestaurantImage> restaurantImageRepository,
            IFileStorageService fileStorageService)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantImageRepository = restaurantImageRepository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Handles the command to upload an image, save it, and store the link in the database.
        /// </summary>
        public async Task<Result<string>> Handle(UploadRestaurantImageCommand request, CancellationToken cancellationToken)
        {
            // Pass the cancellationToken to the async database call.
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                return Result.Failure<string>(new Error("Restaurant.NotFound", $"The restaurant with ID {request.RestaurantId} was not found."));
            }

            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                return Result.Failure<string>(new Error("Image.Empty", "No image file was provided."));
            }

            // Delegate file saving to the storage service
            var imageUrl = await _fileStorageService.SaveFileAsync(request.ImageFile, "restaurants");

            // Create and save the new RestaurantImage entity.
            var restaurantImage = new RestaurantImage
            {
                Id = Guid.NewGuid(),
                Url = imageUrl,
                RestaurantId = request.RestaurantId
            };

            await _restaurantImageRepository.AddAsync(restaurantImage);

            // Pass the cancellationToken to SaveChangesAsync.
            await _restaurantRepository.SaveChangesAsync();

            return Result.Success(imageUrl);
        }
    }
}
