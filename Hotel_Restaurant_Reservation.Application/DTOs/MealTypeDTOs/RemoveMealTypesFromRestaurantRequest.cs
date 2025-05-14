namespace Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;

public class RemoveMealTypesFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
