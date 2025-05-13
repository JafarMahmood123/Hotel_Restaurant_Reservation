using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class FeatureProfile : Profile
{
    public FeatureProfile()
    {
        CreateMap<Feature, FeatureResponse>();
    }
}
