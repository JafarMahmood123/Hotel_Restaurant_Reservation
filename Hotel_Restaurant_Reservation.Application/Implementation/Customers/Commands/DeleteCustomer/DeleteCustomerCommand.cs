using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : ICommand<Result>
{
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}