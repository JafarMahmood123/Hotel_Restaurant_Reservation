﻿namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoomToHotel;

public class AddRoomToHotelRequest
{
    public Guid RoomTypeId { get; set; }
    public int RoomNumber { get; set; }
    public int MaxOccupancy { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}