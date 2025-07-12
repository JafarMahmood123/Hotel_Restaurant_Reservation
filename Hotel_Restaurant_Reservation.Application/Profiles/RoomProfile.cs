using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoomToHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<AddRoomToHotelRequest, Room>();
        CreateMap<Room, RoomResponse>();
    }
}