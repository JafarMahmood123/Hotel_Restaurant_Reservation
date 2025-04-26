using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Features.Commands.AddFeature;

public class AddFeatureCommand : ICommand<Feature>
{
    public Feature Feature { get; set; }

    public AddFeatureCommand(Feature feature)
    {
        Feature = feature;
    }
}
