using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Entities;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Hotels.Queries.GetHotelById;


// Need Editing Later
public class GetHotelByIdQuery : IRequest<Hotel?> 
{
    public Guid Id { get; set; }

    public GetHotelByIdQuery(Guid id)
    {
        Id = id;
    }
}
