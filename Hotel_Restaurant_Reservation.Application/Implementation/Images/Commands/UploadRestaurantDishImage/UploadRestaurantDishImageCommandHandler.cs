using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantDishImage
{
    /// <summary>
    /// Handles the UploadRestaurantDishImageCommand.
    /// </summary>
    public class UploadRestaurantDishImageCommandHandler : ICommandHandler<UploadRestaurantDishImageCommand, Result<string>>
    {
        private readonly IGenericRepository<RestaurantDish> _restaurantDishRepository;
        private readonly IFileStorageService _fileStorageService;

        public UploadRestaurantDishImageCommandHandler(
            IGenericRepository<RestaurantDish> restaurantDishRepository,
            IFileStorageService fileStorageService)
        {
            _restaurantDishRepository = restaurantDishRepository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Handles the command to upload an image, save it, and update the dish's PictureUrl.
        /// </summary>
        public async Task<Result<string>> Handle(UploadRestaurantDishImageCommand request, CancellationToken cancellationToken)
        {
            // Find the specific restaurant dish record.
            var restaurantDish = await _restaurantDishRepository.GetFirstOrDefaultAsync(
                rd => rd.RestaurantId == request.RestaurantId && rd.DishId == request.DishId);

            if (restaurantDish is null)
            {
                return Result.Failure<string>(new Error("Dish.NotFound", "The specified dish was not found for this restaurant."));
            }

            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                return Result.Failure<string>(new Error("Image.Empty", "No image file was provided."));
            }

            // Save the new image using the file storage service.
            // We can store dish images in a "dishes" subfolder.
            var imageUrl = await _fileStorageService.SaveFileAsync(request.ImageFile, "dishes");

            // If the dish already has a picture, delete the old one.
            if (!string.IsNullOrEmpty(restaurantDish.PictureUrl))
            {
                await _fileStorageService.DeleteFileAsync(restaurantDish.PictureUrl);
            }

            // Update the PictureUrl on the dish entity.
            restaurantDish.PictureUrl = imageUrl;
            await _restaurantDishRepository.UpdateAsync(restaurantDish.Id, restaurantDish);

            // Save the changes to the database.
            await _restaurantDishRepository.SaveChangesAsync();

            // Return the URL of the newly uploaded image.
            return Result.Success(imageUrl);
        }
    }
}
