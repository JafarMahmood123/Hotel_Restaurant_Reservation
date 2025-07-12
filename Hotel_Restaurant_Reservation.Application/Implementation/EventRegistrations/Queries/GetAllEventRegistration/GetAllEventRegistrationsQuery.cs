using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistration;

public class GetAllEventRegistrationsQuery : IQuery<Result<IEnumerable<EventRegistrationResponse>>>
{
}