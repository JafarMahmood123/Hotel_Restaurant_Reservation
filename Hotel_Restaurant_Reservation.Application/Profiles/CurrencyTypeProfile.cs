using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyType;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class CurrencyTypeProfile : Profile
{
    public CurrencyTypeProfile()
    {
        CreateMap<CurrencyType, CurrencyTypeResponse>();
    }
}
