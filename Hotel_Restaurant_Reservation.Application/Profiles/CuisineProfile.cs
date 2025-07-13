using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.AddCuisine;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class CuisineProfile : Profile
{

    public CuisineProfile()
    {
        CreateMap<AddCuisineRequest, Cuisine>();

        CreateMap<Cuisine, CuisineResponse>();
    }
}
