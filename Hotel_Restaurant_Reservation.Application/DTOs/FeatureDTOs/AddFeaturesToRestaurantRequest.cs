namespace Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;

public class AddFeaturesToRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
