namespace Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;

public class AddMealTypesToRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
