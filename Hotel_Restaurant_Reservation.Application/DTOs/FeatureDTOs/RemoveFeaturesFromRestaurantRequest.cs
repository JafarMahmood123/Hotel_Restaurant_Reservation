namespace Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;

public class RemoveFeaturesFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
