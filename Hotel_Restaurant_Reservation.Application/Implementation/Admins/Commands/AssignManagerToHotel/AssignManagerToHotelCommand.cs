using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AssignManagerToHotel
{
    public class AssignManagerToHotelCommand : ICommand<Result>
    {
        public Guid HotelId { get; set; }
        public Guid ManagerId { get; set; }
    }
}