using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetAllHotelReservationsByHotelId;

public class GetAllHotelReservationsByHotelIdQueryHandler : IQueryHandler<GetAllHotelReservationsByHotelIdQuery, Result<IEnumerable<HotelReservationResponse>>>
{
    private readonly IGenericRepository<HotelReservation> _hotelReservationRepository;
    private readonly IMapper _mapper;

    public GetAllHotelReservationsByHotelIdQueryHandler(IGenericRepository<HotelReservation> hotelReservationRepository, IMapper mapper)
    {
        _hotelReservationRepository = hotelReservationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<HotelReservationResponse>>> Handle(GetAllHotelReservationsByHotelIdQuery request, CancellationToken cancellationToken)
    {
        var hotelReservations = await _hotelReservationRepository.Where(x => x.HotelId == request.HotelId).ToListAsync(cancellationToken: cancellationToken);
        var hotelReservationResponses = _mapper.Map<IEnumerable<HotelReservationResponse>>(hotelReservations);
        return Result.Success(hotelReservationResponses);
    }
}