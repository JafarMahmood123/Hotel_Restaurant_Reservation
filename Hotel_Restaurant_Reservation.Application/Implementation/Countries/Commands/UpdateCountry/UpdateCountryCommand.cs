using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.UpdateCountry;

public class UpdateCountryCommand : ICommand<Result<CountryResponse>>
{
    public UpdateCountryCommand(Guid id, UpdateCountryRequest updateCountryRequest)
    {
        Id = id;
        UpdateCountryRequest = updateCountryRequest;
    }

    public Guid Id { get; }
    public UpdateCountryRequest UpdateCountryRequest { get; }
}