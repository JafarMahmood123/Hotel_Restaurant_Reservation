using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadUserImages;

public class UploadUserImagesCommand : ICommand<Result<List<string>>>
{
    public Guid UserId { get; set; }
    public List<UploadImageRequest> ImageFiles { get; set; }
}