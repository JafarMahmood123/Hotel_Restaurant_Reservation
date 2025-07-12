using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Commands.AddFeature;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class FeatureProfile : Profile
{

    public FeatureProfile()
    {

        CreateMap<Feature, FeatureResponse>();

        CreateMap<AddFeatureRequest, Feature>();

    }
}
