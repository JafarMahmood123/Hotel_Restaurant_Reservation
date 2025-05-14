using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

public class AddWorkTimesToRestaurantCommand : ICommand<IEnumerable<WorkTime>>
{
    public AddWorkTimesToRestaurantCommand(Guid restaurantId, IEnumerable<Guid> workTimeIds)
    {
        RestaurantId = restaurantId;
        WorkTimeIds = workTimeIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> WorkTimeIds { get; }
}