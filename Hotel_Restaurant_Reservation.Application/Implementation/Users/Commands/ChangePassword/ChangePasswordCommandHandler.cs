using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, Result>
{
    private readonly IGenericRepository<Domain.Entities.User> _customerRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordCommandHandler(IGenericRepository<Domain.Entities.User> customerRepository, IPasswordHasher passwordHasher)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == request.ChangePasswordRequest.Email);

        if (customer is null)
        {
            return Result.Failure(DomainErrors.Customer.LogInUnExistingAccount(request.ChangePasswordRequest.Email));
        }

        if (!_passwordHasher.Verify(request.ChangePasswordRequest.OldPassword, customer.HashedPassword))
        {
            return Result.Failure(DomainErrors.Customer.InvalidOldPassword());
        }

        customer.HashedPassword = _passwordHasher.Hash(request.ChangePasswordRequest.NewPassword);

        await _customerRepository.UpdateAsync(customer.Id, customer);
        await _customerRepository.SaveChangesAsync();

        return Result.Success();
    }
}