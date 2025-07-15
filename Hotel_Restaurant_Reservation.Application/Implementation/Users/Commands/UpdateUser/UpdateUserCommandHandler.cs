using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.UpdateCustomer
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result<UserResponse>>
    {
        private readonly IGenericRepository<Domain.Entities.User> _customerRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IGenericRepository<Domain.Entities.User> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);

            if (customer is null)
            {
                return Result.Failure<UserResponse>(DomainErrors.User.NotFound(request.Id));
            }

            _mapper.Map(request.UpdateCustomerRequest, customer);

            customer.Age = DateTime.Now.Year - customer.BirthDate.Year;

            await _customerRepository.UpdateAsync(request.Id, customer);
            await _customerRepository.SaveChangesAsync();

            var customerResponse = _mapper.Map<UserResponse>(customer);

            return Result.Success(customerResponse);
        }
    }
}