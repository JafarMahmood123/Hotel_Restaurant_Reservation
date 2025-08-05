using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IQuery<Result<PagedResult<UserResponse>>>
    {
        public GetAllUsersQuery(int page = 1, int pageSize = 10)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}
