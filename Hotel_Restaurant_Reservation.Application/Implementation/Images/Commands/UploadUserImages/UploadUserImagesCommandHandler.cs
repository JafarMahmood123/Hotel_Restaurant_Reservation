using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadUserImages;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadUserImage
{
    public class UploadUserImagesCommandHandler : ICommandHandler<UploadUserImagesCommand, Result<List<string>>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<UserImage> _userImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public UploadUserImagesCommandHandler(
            IGenericRepository<User> userRepository,
            IGenericRepository<UserImage> userImageRepository,
            IFileStorageService fileStorageService)
        {
            _userRepository = userRepository;
            _userImageRepository = userImageRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<List<string>>> Handle(UploadUserImagesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
            {
                return Result.Failure<List<string>>(DomainErrors.User.NotFound(request.UserId));
            }

            if (request.ImageFiles == null || !request.ImageFiles.Any())
            {
                return Result.Failure<List<string>>(DomainErrors.User.NoImagesProvided);
            }

            var uploadedUrls = new List<string>();

            const string SUBFOLDER_NAME = "users";

            foreach (var imageFile in request.ImageFiles)
            {
                var imageUrl = await _fileStorageService.SaveFileAsync(imageFile, SUBFOLDER_NAME);

                var userImage = new UserImage
                {
                    Id = Guid.NewGuid(),
                    Url = imageUrl,
                    UserId = request.UserId
                };

                await _userImageRepository.AddAsync(userImage);
                uploadedUrls.Add(imageUrl);
            }

            await _userRepository.SaveChangesAsync();

            return Result.Success(uploadedUrls);
        }
    }
}