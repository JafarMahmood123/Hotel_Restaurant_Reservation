using Hotel_Restaurant_Reservation.Application.Abstractions.JwtProvider;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;

public class LogInCommandHandler : ICommandHandler<LogInCommand, Result<string>>
{
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LogInCommandHandler(IGenericRepository<Customer> customerRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
    {
        _customerRepository = customerRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }


    public async Task<Result<string>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetFirstOrDefaultAsync(
            x => x.Email == request.LogInRequest.Email);

        if (existingCustomer == null)
            return Result.Failure<string>(DomainErrors.Customer.LogInUnExistingAccount(request.LogInRequest.Email));

        if (!_passwordHasher.Verify(request.LogInRequest.Password, existingCustomer.HashedPassword))
            return Result.Failure<string>(DomainErrors.Customer.IncorrectPassword());
        
        string token = _jwtProvider.Generate(existingCustomer, existingCustomer.Role);

        return Result.Success(token);
    }
}
