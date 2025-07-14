using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.UpdateCity;

public class UpdateCityCommand : ICommand<Result<CityResponse>>
{
    public UpdateCityCommand(Guid id, UpdateCityRequest updateCityRequest)
    {
        Id = id;
        UpdateCityRequest = updateCityRequest;
    }

    public Guid Id { get; }
    public UpdateCityRequest UpdateCityRequest { get; }
}