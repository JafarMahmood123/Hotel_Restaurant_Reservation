using Hotel_Restaurant_Reservation.Application.DTOs.RoomAmenity;
using Hotel_Restaurant_Reservation.Application.DTOs.RoomType;

namespace Hotel_Restaurant_Reservation.Application.DTOs.Room;

public class RoomRequest
{
    public int MaxOccupancy { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public RoomTypeRequest RoomTypeRequest { get; set; }

    public IEnumerable<RoomAmenityRequest> RoomAmenityRequests { get; set; }
}
