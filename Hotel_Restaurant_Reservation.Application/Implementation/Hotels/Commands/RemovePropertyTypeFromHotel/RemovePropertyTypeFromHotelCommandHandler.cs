using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.RemovePropertyTypeFromHotel;

 public class RemovePropertyTypeFromHotelCommandHandler : ICommandHandler<RemovePropertyTypeFromHotelCommand, Result>
    {
        private readonly IHotelRepository _hotelRepository;

        public RemovePropertyTypeFromHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Result> Handle(RemovePropertyTypeFromHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
            if (hotel is null)
            {
                return Result.Failure(DomainErrors.Hotel.NotFound(request.HotelId));
            }

            // Set the foreign key to null to remove the association
            hotel.PropertyTypeId = null;

            await _hotelRepository.UpdateAsync(request.HotelId, hotel);
            await _hotelRepository.SaveChangesAsync();

            return Result.Success();
        }
    }