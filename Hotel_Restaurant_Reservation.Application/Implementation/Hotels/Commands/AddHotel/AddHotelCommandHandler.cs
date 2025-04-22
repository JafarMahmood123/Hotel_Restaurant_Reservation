using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;

public class AddHotelCommandHandler : ICommandHandler<AddHotelCommand, Hotel>
{
    private readonly IGenericRepository<Hotel> _genericRepository;

    public AddHotelCommandHandler(IGenericRepository<Hotel> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Hotel> Handle(AddHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = request.Hotel;

        hotel.Id = Guid.NewGuid();

        hotel = await _genericRepository.AddAsync(hotel);

        return hotel;
    }
}
