using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries.GetAllTags
{
    public class GetAllTagsQuery : IQuery<Result<IEnumerable<TagResponse>>>
    {
    }
}