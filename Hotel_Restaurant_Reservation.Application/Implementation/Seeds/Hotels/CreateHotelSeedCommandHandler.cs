using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Text.RegularExpressions;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Seeds.Hotels;

public class CreateHotelSeedCommandHandler : ICommandHandler<CreateHotelSeedCommand, Hotel>
{
    private readonly IGenericRepository<Hotel> _genericRepository;

    public CreateHotelSeedCommandHandler(IGenericRepository<Hotel> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Hotel> Handle(CreateHotelSeedCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _genericRepository.AddAsync(request.Hotel);

        return hotel;
    }
}
