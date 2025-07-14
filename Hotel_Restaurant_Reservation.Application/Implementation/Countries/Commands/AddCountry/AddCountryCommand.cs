using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.AddCountry;

public class AddCountryCommand : ICommand<Result<CountryResponse>>
{
    public AddCountryCommand(AddCountryRequest addCountryRequest)
    {
        AddCountryRequest = addCountryRequest;
    }

    public AddCountryRequest AddCountryRequest { get; }
}