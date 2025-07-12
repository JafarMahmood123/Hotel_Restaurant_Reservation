using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AddEvent;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.UpdateEvent;
using Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<AddEventRequest, Event>();
        CreateMap<Event, EventResponse>();
        CreateMap<UpdateEventRequest, Event>();
    }
}