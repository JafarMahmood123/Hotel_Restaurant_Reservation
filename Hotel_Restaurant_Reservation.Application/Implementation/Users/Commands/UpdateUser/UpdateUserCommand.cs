using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.UpdateCustomer
{
    public class UpdateUserCommand : ICommand<Result<UserResponse>>
    {
        public UpdateUserCommand(Guid id, UpdateUserRequest updateCustomerRequest)
        {
            Id = id;
            UpdateCustomerRequest = updateCustomerRequest;
        }

        public Guid Id { get; }
        public UpdateUserRequest UpdateCustomerRequest { get; }
    }
}