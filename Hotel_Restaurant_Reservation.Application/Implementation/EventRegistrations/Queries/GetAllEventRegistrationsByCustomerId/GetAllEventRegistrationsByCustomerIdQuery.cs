using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries.GetAllEventRegistrationsByCustomerId;

public class GetAllEventRegistrationsByCustomerIdQuery : IQuery<Result<IEnumerable<EventRegistrationResponse>>>
{
    public GetAllEventRegistrationsByCustomerIdQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; }
}