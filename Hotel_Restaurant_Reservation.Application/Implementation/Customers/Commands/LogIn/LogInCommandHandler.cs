using Hotel_Restaurant_Reservation.Application.Abstractions;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;

public class LogInCommandHandler : ICommandHandler<LogInCommand, string?>
{
    private readonly IGenericRepository<Customer> customerRepository;
    private readonly IGenericRepository<Role> roleRepository;
    private readonly IGenericRepository<CustomerRoles> customerRolesRepository;
    private readonly IJwtProvider jwtProvider;

    public LogInCommandHandler(IGenericRepository<Customer> customerRepository,
        IGenericRepository<Role> roleRepository,
        IGenericRepository<CustomerRoles> customerRolesRepository,
        IJwtProvider jwtProvider)
    {
        this.customerRepository = customerRepository;
        this.roleRepository = roleRepository;
        this.customerRolesRepository = customerRolesRepository;
        this.jwtProvider = jwtProvider;
    }


    public async Task<string?> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await customerRepository.GetFirstOrDefaultAsync(
            x => x.Email == request.Email && x.Password == request.Password);

        if(existingCustomer == null) 
            return null;

        //Fetching the roles
        IEnumerable<CustomerRoles>? customerRoles = customerRolesRepository.Where(
            x => x.CustomerId == existingCustomer.Id);


        List<Role> roles = new List<Role>();

        if(customerRoles is not null)
        {
            foreach (var customerRole in customerRoles)
            {
                Role? role = roleRepository.GetById(customerRole.RoleId);

                if(role != null) 
                    roles.Add(role);
            }
        }
        
        string token = jwtProvider.Generate(existingCustomer, roles);

        return token;
    }
}
