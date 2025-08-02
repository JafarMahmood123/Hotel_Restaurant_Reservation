using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        CreateMap<RoomType, RoomTypesResponse>();
    }
}
