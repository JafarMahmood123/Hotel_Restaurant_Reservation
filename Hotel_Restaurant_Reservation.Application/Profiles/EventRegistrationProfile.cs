using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.AddEventRegistration;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.UpdateEventRegistration;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Profiles;

public class EventRegistrationProfile : Profile
{
    public EventRegistrationProfile()
    {
        CreateMap<AddEventRegistrationRequest, EventRegistration>();
        CreateMap<EventRegistration, EventRegistrationResponse>();
        CreateMap<UpdateEventRegistrationRequest, EventRegistration>();
    }
}