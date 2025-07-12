using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.UpdateAmenity;

public class UpdateAmenityCommand : ICommand<Result<AmenityResponse>>
{
    public UpdateAmenityCommand(Guid id, UpdateAmenityRequest updateAmenityRequest)
    {
        Id = id;
        UpdateAmenityRequest = updateAmenityRequest;
    }

    public Guid Id { get; }
    public UpdateAmenityRequest UpdateAmenityRequest { get; }
}