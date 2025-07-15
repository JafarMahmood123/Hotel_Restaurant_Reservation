using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.DeleteCuisine
{
    public class DeleteCuisineCommand : ICommand<Result>
    {
        public DeleteCuisineCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}