using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Commands.AddRoomType;

public class AddRoomTypeCommand : ICommand<Result<RoomTypesResponse>>
{
    public AddRoomTypeCommand(string newRoomType)
    {
        NewRoomType = newRoomType;
    }

    public string NewRoomType { get; }
}
