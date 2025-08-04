using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;

public class RemoveWorkTimesFromRestaurantCommand : ICommand<Result>
{
    public RemoveWorkTimesFromRestaurantCommand(Guid workTimeId)
    {
        WorkTimeId = workTimeId;
    }

    public Guid WorkTimeId { get; }
}