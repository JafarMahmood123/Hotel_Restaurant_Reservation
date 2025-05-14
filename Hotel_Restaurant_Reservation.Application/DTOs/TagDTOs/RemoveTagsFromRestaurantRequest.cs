namespace Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;

public class RemoveTagsFromRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
