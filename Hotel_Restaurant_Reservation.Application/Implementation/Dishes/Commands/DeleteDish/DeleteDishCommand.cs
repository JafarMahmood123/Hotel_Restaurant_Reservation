using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand : ICommand<Result>
    {
        public DeleteDishCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}