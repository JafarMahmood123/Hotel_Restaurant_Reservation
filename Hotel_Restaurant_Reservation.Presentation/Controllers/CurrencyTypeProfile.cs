using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.CurrencyTypeDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class CurrencyTypeProfile : Profile
{
    public CurrencyTypeProfile()
    {
        CreateMap<CurrencyType, CurrencyTypeResponse>();
    }
}
