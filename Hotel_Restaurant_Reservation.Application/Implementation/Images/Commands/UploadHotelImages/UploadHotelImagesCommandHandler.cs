using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadHotelImage;

public class UploadHotelImageCommandHandler : ICommandHandler<UploadHotelImageCommand, Result<string>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IGenericRepository<HotelImage> _hotelImageRepository;
    private readonly IFileStorageService _fileStorageService;

    public UploadHotelImageCommandHandler(
        IHotelRepository hotelRepository,
        IGenericRepository<HotelImage> hotelImageRepository,
        IFileStorageService fileStorageService)
    {
        _hotelRepository = hotelRepository;
        _hotelImageRepository = hotelImageRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<string>> Handle(UploadHotelImageCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        if (hotel is null)
        {
            return Result.Failure<string>(DomainErrors.Hotel.NotFound(request.HotelId));
        }

        if (request.uploadImageApiRequest == null)
        {
            return Result.Failure<string>(DomainErrors.Hotel.NoImagesProvided);
        }

        const string SUBFOLDER_NAME = "hotels";

        var imageUrl = await _fileStorageService.SaveFileAsync(request.uploadImageApiRequest.ImageFile, SUBFOLDER_NAME);

        var hotelImage = new HotelImage
        {
            Id = Guid.NewGuid(),
            Url = imageUrl,
            HotelId = request.HotelId
        };

        await _hotelImageRepository.AddAsync(hotelImage);

        await _hotelRepository.SaveChangesAsync();

        return Result.Success(imageUrl);
    }
}