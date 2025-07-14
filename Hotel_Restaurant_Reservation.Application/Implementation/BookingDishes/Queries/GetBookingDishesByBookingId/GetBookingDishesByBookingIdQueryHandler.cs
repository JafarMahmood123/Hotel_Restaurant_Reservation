using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Queries.GetBookingDishesByBookingId;

public class GetBookingDishesByBookingIdQueryHandler : IQueryHandler<GetBookingDishesByBookingIdQuery, Result<IEnumerable<BookingDishResponse>>>
{
    private readonly IGenericRepository<BookingDish> _bookingDishRepository;
    private readonly IMapper _mapper;

    public GetBookingDishesByBookingIdQueryHandler(IGenericRepository<BookingDish> bookingDishRepository, IMapper mapper)
    {
        _bookingDishRepository = bookingDishRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<BookingDishResponse>>> Handle(GetBookingDishesByBookingIdQuery request, CancellationToken cancellationToken)
    {
        var bookingDishes = await _bookingDishRepository
            .Where(bd => bd.RestaurantBookingId == request.BookingId)
            .ToListAsync(cancellationToken);

        var bookingDishResponses = _mapper.Map<IEnumerable<BookingDishResponse>>(bookingDishes);

        return Result.Success(bookingDishResponses);
    }
}