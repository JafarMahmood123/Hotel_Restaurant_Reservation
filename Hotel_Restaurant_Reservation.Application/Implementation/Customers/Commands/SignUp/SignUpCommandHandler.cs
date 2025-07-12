using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Customers.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;

public class SignUpCommandHandler : ICommandHandler<SignUpCommand, Result<CustomerResponse>>
{
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public SignUpCommandHandler(IGenericRepository<Customer> customerRepository, IMapper mapper)
    {
        this._customerRepository = customerRepository;
        this._mapper = mapper;
    }

    public async Task<Result<CustomerResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request.SignUpRequest);

        var existingCustomer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == customer.Email);

        if (existingCustomer != null)
            return Result.Failure<CustomerResponse>(DomainErrors.Customer.SignUpExistingAccount(request.SignUpRequest.Email));

        customer.Id = Guid.NewGuid();
        customer.Age = DateTime.Now.Year - customer.BirthDate.Year;
        customer.Role = Roles.Customer;

        customer = await _customerRepository.AddAsync(customer);

        await _customerRepository.SaveChangesAsync();

        var customerResponse = _mapper.Map<CustomerResponse>(customer);

        return Result.Success(customerResponse);
    }
}
