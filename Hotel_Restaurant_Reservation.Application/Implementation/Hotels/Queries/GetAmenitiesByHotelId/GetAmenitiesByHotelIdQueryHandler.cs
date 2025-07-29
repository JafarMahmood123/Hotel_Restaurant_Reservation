using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAmenitiesByHotelId;

public class GetAmenitiesByHotelIdQueryHandler : IQueryHandler<GetAmenitiesByHotelIdQuery, Result<IEnumerable<GetAmenitiesByHotelIdResponse>>>
{
    private readonly IGenericRepository<HotelAmenityPrice> _hotelAmenityPriceRepository;
    private readonly IMapper _mapper;

    public GetAmenitiesByHotelIdQueryHandler(IGenericRepository<HotelAmenityPrice> hotelAmenityPriceRepository
        ,IMapper mapper)
    {
        _hotelAmenityPriceRepository = hotelAmenityPriceRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetAmenitiesByHotelIdResponse>>> Handle(GetAmenitiesByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var hotelAmenitiesWithPrices = await _hotelAmenityPriceRepository
            .Where(x => x.HotelId == request.HotelId)
            .Include(x => x.Amenity).ToListAsync();

        var result = _mapper.Map<List<GetAmenitiesByHotelIdResponse>>(hotelAmenitiesWithPrices);

        return Result.Success((IEnumerable<GetAmenitiesByHotelIdResponse>)result);
    }
}
