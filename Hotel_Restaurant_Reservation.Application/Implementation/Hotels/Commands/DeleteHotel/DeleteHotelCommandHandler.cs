using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommandHandler : ICommandHandler<DeleteHotelCommand, HotelResponse?>
{
    private readonly IHotelRepository hotelRepository;
    private readonly IMapper mapper;

    public DeleteHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        this.hotelRepository = hotelRepository;
        this.mapper = mapper;
    }

    public async Task<HotelResponse?> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        var hotelId = request.HotelId;

        var hotel = await hotelRepository.RemoveAsync(hotelId);

        if (hotel == null) 
            return null;

        await hotelRepository.SaveChangesAsync();

        return mapper.Map<HotelResponse>(hotel);
    }
}
