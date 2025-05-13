using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.AddCountry;

public class AddCountryCommand : ICommand<Country>
{
    public Country Country { get; set; }

    public AddCountryCommand(Country country)
    {
        Country = country;
    }
}
