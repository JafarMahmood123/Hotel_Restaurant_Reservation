using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadHotelImages;

public class UploadHotelImagesCommandHandler : ICommandHandler<UploadHotelImagesCommand, Result<List<string>>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IGenericRepository<HotelImage> _hotelImageRepository;
    private readonly IFileStorageService _fileStorageService;

    public UploadHotelImagesCommandHandler(
        IHotelRepository hotelRepository,
        IGenericRepository<HotelImage> hotelImageRepository,
        IFileStorageService fileStorageService)
    {
        _hotelRepository = hotelRepository;
        _hotelImageRepository = hotelImageRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<List<string>>> Handle(UploadHotelImagesCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        if (hotel is null)
        {
            return Result.Failure<List<string>>(DomainErrors.Hotel.NotFound(request.HotelId));
        }

        if (request.ImageFiles == null || !request.ImageFiles.Any())
        {
            return Result.Failure<List<string>>(DomainErrors.Hotel.NoImagesProvided);
        }

        var uploadedUrls = new List<string>();

        const string SUBFOLDER_NAME = "hotels";

        foreach (var imageFile in request.ImageFiles)
        {
            var imageUrl = await _fileStorageService.SaveFileAsync(imageFile, SUBFOLDER_NAME);

            var hotelImage = new HotelImage
            {
                Id = Guid.NewGuid(),
                Url = imageUrl,
                HotelId = request.HotelId
            };

            await _hotelImageRepository.AddAsync(hotelImage);
            uploadedUrls.Add(imageUrl);
        }

        await _hotelRepository.SaveChangesAsync();

        return Result.Success(uploadedUrls);
    }
}