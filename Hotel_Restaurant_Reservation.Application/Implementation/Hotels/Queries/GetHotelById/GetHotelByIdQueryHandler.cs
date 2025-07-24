using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;

public class GetHotelByIdQueryHandler : IQueryHandler<GetHotelByIdQuery, Result<HotelResponse>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public GetHotelByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<Result<HotelResponse>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id);

        if (hotel is null)
        {
            return Result.Failure<HotelResponse>(DomainErrors.Hotel.NotFound(request.Id));
        }

        var hotelResponse = _mapper.Map<HotelResponse>(hotel);
        return Result.Success(hotelResponse);
    }
}