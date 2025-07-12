using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Features.Commands.AddFeature;

public class AddFeatureCommand : ICommand<Result<FeatureResponse>>
{
    public AddFeatureCommand(AddFeatureRequest addFeatureRequest)
    {
        AddFeatureRequest = addFeatureRequest;
    }

    public AddFeatureRequest AddFeatureRequest { get; }
}
