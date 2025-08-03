using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries.GetAllFeatures;

public class GetAllFeaturesQuery : IQuery<Result<IEnumerable<FeatureResponse>>>
{

}
