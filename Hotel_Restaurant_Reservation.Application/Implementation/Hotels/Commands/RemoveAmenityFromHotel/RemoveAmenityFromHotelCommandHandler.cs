using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.RemoveAmenityFromHotel;

public class RemoveAmenityFromHotelCommandHandler : ICommandHandler<RemoveAmenityFromHotelCommand, Result>
{
    private readonly IGenericRepository<HotelAmenityPrice> _hotelAmenityPriceRepository;

    public RemoveAmenityFromHotelCommandHandler(IGenericRepository<HotelAmenityPrice> hotelAmenityPriceRepository)
    {
        this._hotelAmenityPriceRepository = hotelAmenityPriceRepository;
    }

    public async Task<Result> Handle(RemoveAmenityFromHotelCommand request, CancellationToken cancellationToken)
    {
        var hotelAmenity = await _hotelAmenityPriceRepository.GetFirstOrDefaultAsync(x => x.AmenityId == request.AmenityId
        && x.HotelId == request.HotelId);

        if(hotelAmenity == null)
        {
            return Result.Failure(DomainErrors.Hotel.DontHaveAmenity);
        }

        await _hotelAmenityPriceRepository.RemoveAsync(hotelAmenity.Id);
        await _hotelAmenityPriceRepository.SaveChangesAsync();

        return Result.Success();
    }
}
