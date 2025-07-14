using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;

public class AddLocalLocationCommand : ICommand<Result<LocalLocationResponse>>
{
    public AddLocalLocationCommand(AddLocalLocationRequest addLocalLocationRequest)
    {
        AddLocalLocationRequest = addLocalLocationRequest;
    }

    public AddLocalLocationRequest AddLocalLocationRequest { get; }
}