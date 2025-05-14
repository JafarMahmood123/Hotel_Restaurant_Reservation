namespace Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;

public class RemoveWorkTimesFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
