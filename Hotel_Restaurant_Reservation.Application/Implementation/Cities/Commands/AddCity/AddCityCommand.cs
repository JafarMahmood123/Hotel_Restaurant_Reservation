using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;

public class AddCityCommand : ICommand<Result<CityResponse>>
{
    public AddCityCommand(AddCityRequest addCityRequest)
    {
        AddCityRequest = addCityRequest;
    }

    public AddCityRequest AddCityRequest { get; }
}