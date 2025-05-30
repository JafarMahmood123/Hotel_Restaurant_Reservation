using Hotel_Restaurant_Reservation.Application.Abstractions;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;

public class LogInCommandHandler : ICommandHandler<LogInCommand, Result<string>>
{
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IJwtProvider _jwtProvider;

    public LogInCommandHandler(IGenericRepository<Customer> customerRepository, IJwtProvider jwtProvider)
    {
        this._customerRepository = customerRepository;
        this._jwtProvider = jwtProvider;
    }


    public async Task<Result<string>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetFirstOrDefaultAsync(
            x => x.Email == request.LogInRequest.Email);

        if (existingCustomer == null)
            return Result.Failure<string>(DomainErrors.Customer.LogInUnExistingAccount(request.LogInRequest.Email));

        if (existingCustomer.Password != request.LogInRequest.Password)
            return Result.Failure<string>(DomainErrors.Customer.IncorrectPassword());
        
        string token = _jwtProvider.Generate(existingCustomer, existingCustomer.Role);

        return Result.Success(token);
    }
}
