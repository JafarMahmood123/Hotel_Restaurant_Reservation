using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.SignUp;

public class SignUpCommandHandler : ICommandHandler<SignUpCommand, Result<UserResponse>>
{
    private readonly IGenericRepository<Domain.Entities.User> _customerRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public SignUpCommandHandler(IGenericRepository<Domain.Entities.User> customerRepository, IGenericRepository<Role> roleRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _customerRepository = customerRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Domain.Entities.User>(request.SignUpRequest);

        var existingCustomer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == customer.Email);

        if (existingCustomer != null)
            return Result.Failure<UserResponse>(DomainErrors.Customer.SignUpExistingAccount(request.SignUpRequest.Email));

        const string CUSTOMER_ROLE_NAME = "customer";

        var customerRole = await _roleRepository.GetFirstOrDefaultAsync(r => r.Name.ToUpper().Equals(CUSTOMER_ROLE_NAME.ToUpper()));
        if (customerRole == null)
        {
            return Result.Failure<UserResponse>(DomainErrors.Role.CustomerRoleNotFound());
        }

        customer.Id = Guid.NewGuid();
        customer.Age = DateTime.Now.Year - customer.BirthDate.Year;
        customer.RoleId = customerRole.Id;
        customer.HashedPassword = _passwordHasher.Hash(request.SignUpRequest.Password);

        customer = await _customerRepository.AddAsync(customer);

        await _customerRepository.SaveChangesAsync();

        var customerResponse = _mapper.Map<UserResponse>(customer);

        return Result.Success(customerResponse);
    }
}