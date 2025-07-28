using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Application.Features.Tags.Queries.GetTagsByRestaurantId;

public sealed record GetTagsByRestaurantIdQuery(Guid RestaurantId) : IQuery<Result<IEnumerable<GetTagsByRestaurantIdResponse>>>;