using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Admins.Commands.AddAdmin;

public class AddAdminCommandHandler : ICommandHandler<AddAdminCommand, Result>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public AddAdminCommandHandler(IGenericRepository<User> userRepository,
        IGenericRepository<Role> roleRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<Result> Handle(AddAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = _mapper.Map<Domain.Entities.User>(request.AddAdminRequest);

        var existingdmin = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == admin.Email);

        if (existingdmin != null)
            return Result.Failure<UserResponse>(DomainErrors.User.AddAdminExistingAccount(admin.Email));

        const string ADMIN_ROLE_NAME = "admin";

        var adminRole = await _roleRepository.GetFirstOrDefaultAsync(r => r.Name.ToUpper().Equals(ADMIN_ROLE_NAME.ToUpper()));
        if (adminRole == null)
        {
            return Result.Failure<UserResponse>(DomainErrors.Role.AdminRoleNotFound());
        }
        admin.RoleId = adminRole.Id;

        admin.Id = Guid.NewGuid();
        admin.Age = DateTime.Now.Year - admin.BirthDate.Year;

        admin.HashedPassword = _passwordHasher.Hash(request.AddAdminRequest.Password);

        admin = await _userRepository.AddAsync(admin);

        await _userRepository.SaveChangesAsync();

        return Result.Success();
    }
}
