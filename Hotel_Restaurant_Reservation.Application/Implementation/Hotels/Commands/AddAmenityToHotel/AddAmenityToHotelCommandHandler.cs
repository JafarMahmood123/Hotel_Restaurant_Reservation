using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAmenitiesByHotelId;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddAmenityToHotel;

public class AddAmenityToHotelCommandHandler : ICommandHandler<AddAmenityToHotelCommand, Result<GetAmenitiesByHotelIdResponse>>
{
    private readonly IGenericRepository<HotelAmenityPrice> _hotelAmenityRepository;
    private readonly IMapper _mapper;

    public AddAmenityToHotelCommandHandler(
        IGenericRepository<HotelAmenityPrice> hotelAmenityRepository,
        IMapper mapper)
    {
        _hotelAmenityRepository = hotelAmenityRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetAmenitiesByHotelIdResponse>> Handle(AddAmenityToHotelCommand request, CancellationToken cancellationToken)
    {
        var existingHotelAmenity = await _hotelAmenityRepository.GetFirstOrDefaultAsync(x => x.HotelId == request.HotelId
        && x.AmenityId == request.AmenityId);

        if(existingHotelAmenity != null)
        {
            return Result.Failure<GetAmenitiesByHotelIdResponse>(DomainErrors.Hotel.ExistingAmenity);
        }

        var hotelAmenity = new HotelAmenityPrice();

        hotelAmenity.Id = Guid.NewGuid();
        hotelAmenity.HotelId = request.HotelId;
        hotelAmenity.AmenityId = request.AmenityId;
        hotelAmenity.Price = request.AddAmenityToHotelRequest.Price;

        hotelAmenity = await _hotelAmenityRepository.AddAsync(hotelAmenity);

        await _hotelAmenityRepository.SaveChangesAsync();
        var result = _mapper.Map<GetAmenitiesByHotelIdResponse>(hotelAmenity);

        return Result.Success(result);

    }
}
