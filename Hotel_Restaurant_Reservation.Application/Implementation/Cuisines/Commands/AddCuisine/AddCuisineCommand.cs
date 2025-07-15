using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.AddCuisine
{
    public class AddCuisineCommand : ICommand<Result<CuisineResponse>>
    {
        public AddCuisineCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}