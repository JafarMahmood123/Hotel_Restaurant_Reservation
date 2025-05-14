using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;

public class RemoveWorkTimesFromRestaurantCommand : ICommand<IEnumerable<WorkTime>>
{
    public RemoveWorkTimesFromRestaurantCommand(Guid restaurantId, IEnumerable<Guid> workTimeIds)
    {
        RestaurantId = restaurantId;
        WorkTimeIds = workTimeIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> WorkTimeIds { get; }
}
