namespace Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;

public class RemoveDishesFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
