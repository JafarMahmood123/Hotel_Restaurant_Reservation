using Hotel_Restaurant_Reservation.Application.Abstractions.JwtProvider;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.LogIn;

public class LogInCommandHandler : ICommandHandler<LogInCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.User> _customerRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LogInCommandHandler(IGenericRepository<Domain.Entities.User> customerRepository, IGenericRepository<Role> roleRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
    {
        _customerRepository = customerRepository;
        _roleRepository = roleRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }


    public async Task<Result<string>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetFirstOrDefaultAsync(
            x => x.Email == request.LogInRequest.Email);

        if (existingCustomer == null)
            return Result.Failure<string>(DomainErrors.User.LogInUnExistingAccount(request.LogInRequest.Email));

        if (!_passwordHasher.Verify(request.LogInRequest.Password, existingCustomer.HashedPassword))
            return Result.Failure<string>(DomainErrors.User.IncorrectPassword());

        var role = await _roleRepository.GetByIdAsync(existingCustomer.RoleId);
        if (role is null)
        {
            return Result.Failure<string>(DomainErrors.Role.NotFound(existingCustomer.RoleId));
        }

        string token = _jwtProvider.Generate(existingCustomer, role);

        return Result.Success(token);
    }
}