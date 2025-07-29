using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetRoomsByHotelId;

public class GetRoomsByHotelIdResponse
{
    public Guid Id { get; set; }
    public int RoomNumber { get; set; }

    public int MaxOccupancy { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public string RoomTypeDescription { get; set; }
}
