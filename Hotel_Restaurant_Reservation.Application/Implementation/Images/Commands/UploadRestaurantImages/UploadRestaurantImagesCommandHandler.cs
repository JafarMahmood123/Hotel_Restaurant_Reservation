using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage
{
    public class UploadRestaurantImagesCommandHandler : ICommandHandler<UploadRestaurantImagesCommand, Result<List<string>>>
    {
        private readonly IRestaurantRespository _restaurantRepository;
        private readonly IGenericRepository<RestaurantImage> _restaurantImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public UploadRestaurantImagesCommandHandler(
            IRestaurantRespository restaurantRepository,
            IGenericRepository<RestaurantImage> restaurantImageRepository,
            IFileStorageService fileStorageService)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantImageRepository = restaurantImageRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<List<string>>> Handle(UploadRestaurantImagesCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
            {
                return Result.Failure<List<string>>(DomainErrors.Restaurant.NotFound(request.RestaurantId));
            }

            if (request.ImageFiles == null || !request.ImageFiles.Any())
            {
                return Result.Failure<List<string>>(DomainErrors.Restaurant.NoImagesProvided);
            }

            var uploadedUrls = new List<string>();

            const string SUBFOLDER_NAME = "restaurants";

            foreach (var imageFile in request.ImageFiles)
            {
                var imageUrl = await _fileStorageService.SaveFileAsync(imageFile, SUBFOLDER_NAME);

                var restaurantImage = new RestaurantImage
                {
                    Id = Guid.NewGuid(),
                    Url = imageUrl,
                    RestaurantId = request.RestaurantId
                };

                await _restaurantImageRepository.AddAsync(restaurantImage);
                uploadedUrls.Add(imageUrl);
            }

            await _restaurantRepository.SaveChangesAsync();

            return Result.Success(uploadedUrls);
        }
    }
}