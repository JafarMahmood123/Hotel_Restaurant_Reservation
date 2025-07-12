using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.AddAmenity;

public class AddAmenityCommand : ICommand<Result<AmenityResponse>>
{
    public AddAmenityCommand(AddAmenityRequest addAmenityRequest)
    {
        AddAmenityRequest = addAmenityRequest;
    }

    public AddAmenityRequest AddAmenityRequest { get; }
}