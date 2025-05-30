namespace Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;

public class RemoveCuisineFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
