using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.BookingDishDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.BookingDishes.Commands.AddBookingDishes;

public class AddBookingDishesCommandHandler : ICommandHandler<AddBookingDishesCommand, IEnumerable<BookingDishResponse>>
{
    private readonly IGenericRepository<BookingDish> _bookingDishRepository;
    private readonly IMapper _mapper;

    public AddBookingDishesCommandHandler(IGenericRepository<BookingDish> bookingDishRepository, IMapper mapper)
    {
        _bookingDishRepository = bookingDishRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookingDishResponse>> Handle(AddBookingDishesCommand request,
        CancellationToken cancellationToken)
    {
        var bookingId = request.BookingId;
        var dishesIdsWithQuantities = request.AddBookingDishesRequest.dishesIdsWithQuantities;

        var bookingDishesResponses = new List<BookingDishResponse>();

        foreach (var dishId in dishesIdsWithQuantities.Keys)
        {
            var bookingDish = new BookingDish()
            {
                Id = Guid.NewGuid(),
                DishId = dishId,
                Quantity = dishesIdsWithQuantities[dishId]
            };

            bookingDish = await _bookingDishRepository.AddAsync(bookingDish);

            await _bookingDishRepository.SaveChangesAsync();

            bookingDishesResponses.Add(_mapper.Map<BookingDishResponse>(bookingDish));
        }

        return bookingDishesResponses;
    }
}
