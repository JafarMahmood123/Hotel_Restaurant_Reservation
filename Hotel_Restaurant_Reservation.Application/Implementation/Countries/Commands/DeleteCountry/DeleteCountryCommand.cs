using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.DeleteCountry;

public class DeleteCountryCommand : ICommand<Country?>
{
    public DeleteCountryCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
