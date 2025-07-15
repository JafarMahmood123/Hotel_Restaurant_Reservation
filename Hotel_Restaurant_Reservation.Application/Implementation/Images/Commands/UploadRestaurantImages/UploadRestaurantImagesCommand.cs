using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadRestaurantImage;


public class UploadRestaurantImagesCommand : ICommand<Result<List<string>>>
{
    public Guid RestaurantId { get; set; }
    public List<UploadRestaurantImagesRequest> ImageFiles { get; set; }
}
