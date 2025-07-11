using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AssignPropertyTypeToHotel;

public class AssignPropertyTypeToHotelCommand : ICommand<Result<PropertyTypeResponse>>
{
    public AssignPropertyTypeToHotelCommand(Guid hotelId, Guid propertyTypeId)
    {
        HotelId = hotelId;
        PropertyTypeId = propertyTypeId;
    }

    public Guid HotelId { get; }
    public Guid PropertyTypeId { get; }
}