using AutoMapper;
using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Presentation.Profiles;

public class TagProfile : Profile
{

    public TagProfile()
    {

        CreateMap<Tag, TagResponse>();

        CreateMap<TagRequest, Tag>();
    }
}
