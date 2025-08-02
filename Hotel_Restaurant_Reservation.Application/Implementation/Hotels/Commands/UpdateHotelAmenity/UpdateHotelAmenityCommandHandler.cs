using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotelAmenity;

public class UpdateHotelAmenityCommandHandler : ICommandHandler<UpdateHotelAmenityCommand, Result>
{
    private readonly IGenericRepository<HotelAmenityPrice> _hotelAmenityRepository;

    public UpdateHotelAmenityCommandHandler(IGenericRepository<HotelAmenityPrice> hotelAmenityRepository)
    {
        _hotelAmenityRepository = hotelAmenityRepository;
    }

    public async Task<Result> Handle(UpdateHotelAmenityCommand request, CancellationToken cancellationToken)
    {
        var hotelAmenity = await _hotelAmenityRepository.GetFirstOrDefaultAsync(x => x.AmenityId == request.AmenityId
        && x.HotelId == request.HotelId);

        if(hotelAmenity == null)
        {
            return Result.Failure(DomainErrors.Hotel.DontHaveAmenity);
        }

        hotelAmenity.Price = request.NewPrice;

        await _hotelAmenityRepository.UpdateAsync(hotelAmenity.Id, hotelAmenity);

        await _hotelAmenityRepository.SaveChangesAsync();

        return Result.Success();
    }
}
