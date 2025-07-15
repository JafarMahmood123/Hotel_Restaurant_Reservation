using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.AddAdmin;

public class AddAdminCommandHandler : ICommandHandler<AddAdminCommand, Result<UserResponse>>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public AddAdminCommandHandler(
        IGenericRepository<User> userRepository,
        IGenericRepository<Role> roleRepository,
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserResponse>> Handle(AddAdminCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.AddAdminRequest);

        var existingUser = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == user.Email);

        if (existingUser != null)
            return Result.Failure<UserResponse>(DomainErrors.User.SignUpExistingAccount(request.AddAdminRequest.Email));

        const string ADMIN_ROLE_NAME = "admin";

        var adminRole = await _roleRepository.GetFirstOrDefaultAsync(r => r.Name.ToUpper() == ADMIN_ROLE_NAME.ToUpper());
        if (adminRole == null)
        {
            return Result.Failure<UserResponse>(DomainErrors.Role.AdminRoleNotFound());
        }

        user.Id = Guid.NewGuid();
        user.Age = DateTime.Now.Year - user.BirthDate.Year;
        user.RoleId = adminRole.Id;
        user.HashedPassword = _passwordHasher.Hash(request.AddAdminRequest.Password);

        user = await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var userResponse = _mapper.Map<UserResponse>(user);

        return Result.Success(userResponse);
    }
}