using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadEventImage;


public class UploadEventImagesCommand : ICommand<Result<List<string>>>
{
    public Guid EventId { get; set; }
    public List<UploadImageRequest> ImageFiles { get; set; }
}