using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetAllHotelReservationsByCustomerId;

public class GetAllHotelReservationsByCustomerIdQueryHandler : IQueryHandler<GetAllHotelReservationsByCustomerIdQuery, Result<IEnumerable<HotelReservationResponse>>>
{
    private readonly IGenericRepository<HotelReservation> _hotelReservationRepository;
    private readonly IMapper _mapper;

    public GetAllHotelReservationsByCustomerIdQueryHandler(IGenericRepository<HotelReservation> hotelReservationRepository, IMapper mapper)
    {
        _hotelReservationRepository = hotelReservationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<HotelReservationResponse>>> Handle(GetAllHotelReservationsByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var hotelReservations = await _hotelReservationRepository.Where(x => x.CustomerId == request.CustomerId).ToListAsync(cancellationToken: cancellationToken);
        var hotelReservationResponses = _mapper.Map<IEnumerable<HotelReservationResponse>>(hotelReservations);
        return Result.Success(hotelReservationResponses);
    }
}