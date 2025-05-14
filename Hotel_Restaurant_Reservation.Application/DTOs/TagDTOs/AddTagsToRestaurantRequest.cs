namespace Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;

public class AddTagsToRestaurantRequest
{
    public IEnumerable<Guid> Ids { get; set; }
}
