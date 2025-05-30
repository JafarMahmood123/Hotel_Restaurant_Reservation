using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Controllers;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagResponse>();
    }
}
