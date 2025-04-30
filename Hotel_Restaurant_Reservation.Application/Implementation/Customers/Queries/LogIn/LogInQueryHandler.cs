using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Queries.LogIn;

public class LogInQueryHandler : ICommandHandler<LogInQuery, Customer?>
{
    private readonly IGenericRepository<Customer> customerRepository;

    public LogInQueryHandler(IGenericRepository<Customer> customerRepository)
    {
        this.customerRepository = customerRepository;
    }


    public async Task<Customer?> Handle(LogInQuery request, CancellationToken cancellationToken)
    {
        var existingCustomer = await customerRepository.GetFirstOrDefaultAsync(
            x => x.Email == request.Email && x.Password == request.Password);

        return existingCustomer;
    }
}
