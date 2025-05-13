namespace Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;

public class AddDishesWithPricesToRestaurantRequest
{
    public Dictionary<Guid, double> dishIdsWithPrices { get; set; }
}
