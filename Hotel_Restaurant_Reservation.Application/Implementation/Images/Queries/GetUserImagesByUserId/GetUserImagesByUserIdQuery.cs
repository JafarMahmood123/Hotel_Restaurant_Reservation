using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetUserImagesByUserId
{
    public class GetUserImagesByUserIdQuery : IQuery<Result<List<string>>>
    {
        public GetUserImagesByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}