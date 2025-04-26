using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.UpdateCountry;

public class UpdateCountryCommand : ICommand<Country?>
{
    public UpdateCountryCommand(Guid id, Country country)
    {
        Id = id;
        Country = country;
    }

    public Guid Id { get; }
    public Country Country { get; }
}
