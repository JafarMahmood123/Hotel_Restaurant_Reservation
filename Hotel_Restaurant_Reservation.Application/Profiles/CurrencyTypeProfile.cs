using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.AddCurrencyType;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class CurrencyTypeProfile : Profile
{
    public CurrencyTypeProfile()
    {
        CreateMap<CurrencyType, CurrencyTypeResponse>();
        CreateMap<AddCurrencyTypeRequest, CurrencyType>();
    }
}