using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Hotel_Restaurant_Reservation.Presentation.ApiModels;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadHotelImage;

public class UploadHotelImageCommand : ICommand<Result<string>>
{
    public UploadHotelImageCommand(Guid hotelId, UploadImageApiRequest uploadImageApiRequest)
    {
        HotelId = hotelId;
        this.uploadImageApiRequest = uploadImageApiRequest;
    }

    public Guid HotelId { get; set; }
    public UploadImageApiRequest uploadImageApiRequest { get; set; }
}