using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IQuery<Result<IEnumerable<UserResponse>>>
{
}
