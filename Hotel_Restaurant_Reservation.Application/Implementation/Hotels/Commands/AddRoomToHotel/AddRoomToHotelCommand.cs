using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoomToHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoom;

public class AddRoomToHotelCommand : ICommand<Result<RoomResponse>>
{
    public AddRoomToHotelCommand(Guid hotelId, AddRoomToHotelRequest addRoomRequest)
    {
        HotelId = hotelId;
        AddRoomRequest = addRoomRequest;
    }

    public Guid HotelId { get; }
    public AddRoomToHotelRequest AddRoomRequest { get; }
}