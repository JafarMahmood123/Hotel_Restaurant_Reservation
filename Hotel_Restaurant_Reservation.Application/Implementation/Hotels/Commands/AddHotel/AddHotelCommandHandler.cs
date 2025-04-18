using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;

public class AddHotelCommandHandler : ICommandHandler<AddHotelCommand, Hotel>
{
    private readonly IGenericRepository<Hotel> _genericRepository;
    private readonly IMapper _mapper;

    public AddHotelCommandHandler(IGenericRepository<Hotel> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<Hotel> Handle(AddHotelCommand request, CancellationToken cancellationToken)
    {
        HotelAddRequest hotelAddRequest = request.HotelAddRequest;

        Hotel hotel = _mapper.Map<Hotel>(hotelAddRequest);

        hotel.Id = Guid.NewGuid();

        hotel = await _genericRepository.AddAsync(hotel);

        return hotel;
    }
}
