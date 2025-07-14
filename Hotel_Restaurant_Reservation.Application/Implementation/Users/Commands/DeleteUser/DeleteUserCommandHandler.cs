using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.DeleteCustomer;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Result>
{
    private readonly IGenericRepository<Domain.Entities.User> _customerRepository;

    public DeleteUserCommandHandler(IGenericRepository<Domain.Entities.User> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer is null)
        {
            return Result.Failure(DomainErrors.Customer.NotFound(request.Id));
        }

        await _customerRepository.RemoveAsync(request.Id);
        await _customerRepository.SaveChangesAsync();

        return Result.Success();
    }
}