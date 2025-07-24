using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler : IQueryHandler<GetAllHotelsQuery, Result<IEnumerable<HotelResponse>>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public GetAllHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<HotelResponse>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        var hotels = await _hotelRepository.GetAllAsync();
        var hotelResponses = _mapper.Map<IEnumerable<HotelResponse>>(hotels);
        return Result.Success(hotelResponses);
    }
}