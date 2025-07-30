using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AssignManagerToHotel
{
    public class AssignManagerToHotelCommandHandler : ICommandHandler<AssignManagerToHotelCommand, Result>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IGenericRepository<User> _userRepository;

        public AssignManagerToHotelCommandHandler(IHotelRepository hotelRepository, IGenericRepository<User> userRepository)
        {
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(AssignManagerToHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
            if (hotel == null)
            {
                return Result.Failure(DomainErrors.Hotel.NotFound(request.HotelId));
            }

            await _hotelRepository.UpdateAsync(request.HotelId, hotel);
            await _hotelRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}