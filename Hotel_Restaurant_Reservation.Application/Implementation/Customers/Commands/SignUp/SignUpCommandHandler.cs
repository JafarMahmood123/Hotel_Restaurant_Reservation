using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;

public class SignUpCommandHandler : ICommandHandler<SignUpCommand, Customer?>
{
    private readonly IGenericRepository<Customer> customerRepository;

    public SignUpCommandHandler(IGenericRepository<Customer> customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task<Customer?> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var existingEmail = await customerRepository.GetFirstOrDefaultAsync(x => x.Email == request.Customer.Email);

        if (existingEmail != null)
            return null;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - request.Customer.BirthDate.Year;

        // Subtract a year if the birthday hasn't occurred yet this year
        if (request.Customer.BirthDate > today.AddYears(-age))
        {
            age--;
        }

        request.Customer.Age = age;
        request.Customer.Id = Guid.NewGuid();

        var newCustomer = await customerRepository.AddAsync(request.Customer);

        await customerRepository.SaveChangesAsync();

        return newCustomer;
    }
}
