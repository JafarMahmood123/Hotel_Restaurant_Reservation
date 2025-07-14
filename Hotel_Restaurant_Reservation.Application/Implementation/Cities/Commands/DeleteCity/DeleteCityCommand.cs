using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.DeleteCity;

public class DeleteCityCommand : ICommand<Result>
{
    public DeleteCityCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}