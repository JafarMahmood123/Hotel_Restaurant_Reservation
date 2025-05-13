using Hotel_Restaurant_Reservation.Application.DTOs.RoomAmenity;
using Hotel_Restaurant_Reservation.Application.DTOs.RoomTypeDTOs;

namespace Hotel_Restaurant_Reservation.Application.DTOs.RoomDTOs;

public class RoomRequest
{
    public int MaxOccupancy { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public RoomTypeRequest RoomTypeRequest { get; set; }

    public IEnumerable<RoomAmenityRequest> RoomAmenityRequests { get; set; }
}
