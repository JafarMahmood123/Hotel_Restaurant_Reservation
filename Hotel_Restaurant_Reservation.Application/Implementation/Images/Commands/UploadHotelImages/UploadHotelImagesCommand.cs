using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadHotelImages;

public class UploadHotelImagesCommand : ICommand<Result<List<string>>>
{
    public Guid HotelId { get; set; }
    public List<UploadImageRequest> ImageFiles { get; set; }
}