namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;

public class RoomResponse
{
    public Guid Id { get; set; }
    public int RoomNumber { get; set; }
    public int MaxOccupancy { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
}