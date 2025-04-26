using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.DeleteCity;

public class DeleteCityCommand : ICommand<City?>
{
    public Guid Id { get; set; }

    public DeleteCityCommand(Guid id)
    {
        Id = id;
    }
}
