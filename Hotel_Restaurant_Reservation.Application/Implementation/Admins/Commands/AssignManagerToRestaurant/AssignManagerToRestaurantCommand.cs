using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AssignManagerToRestaurant
{
    public class AssignManagerToRestaurantCommand : ICommand<Result>
    {
        public Guid RestaurantId { get; set; }
        public Guid ManagerId { get; set; }
    }
}