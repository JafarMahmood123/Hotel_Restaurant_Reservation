using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : ICommandHandler<UpdateRoomCommand, Result<RoomResponse>>
    {
        private readonly IGenericRepository<Room> _roomRepository;
        private readonly IGenericRepository<Hotel> _hotelRepository; // Added repository for Hotel
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(
            IGenericRepository<Room> roomRepository,
            IGenericRepository<Hotel> hotelRepository, // Injected repository
            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomResponse>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetFirstOrDefaultAsync(r => r.Id == request.Id);

            if (room is null)
            {
                return Result.Failure<RoomResponse>(DomainErrors.Room.NotFound(request.Id));
            }

            // 1. Room Number Uniqueness Validation
            if (request.UpdateRoomRequest.RoomNumber != room.RoomNumber)
            {
                // Check if another room in the same hotel already has this number
                var roomNumberExists = await _roomRepository
                    .GetFirstOrDefaultAsync(r => r.HotelId == room.HotelId &&
                                      r.RoomNumber == request.UpdateRoomRequest.RoomNumber &&
                                      r.Id != request.Id);

                if (roomNumberExists != null)
                {
                    return Result.Failure<RoomResponse>(DomainErrors.Room.NumberAlreadyExists);
                }
            }

            var oldPrice = room.Price;
            var newPrice = request.UpdateRoomRequest.Price;

            // Map the requested changes onto the room entity
            _mapper.Map(request.UpdateRoomRequest, room);

            // 2. Hotel Price Range Update
            if (newPrice != oldPrice)
            {
                var hotel = await _hotelRepository.GetByIdAsync(room.HotelId);
                if (hotel != null)
                {
                    // Get all rooms for the hotel to recalculate price range
                    var hotelRooms = await _roomRepository.Where(r => r.HotelId == room.HotelId).ToListAsync(cancellationToken);

                    // Create a list of all prices, ensuring the new price for the current room is included
                    var prices = hotelRooms.Select(r => r.Id == room.Id ? newPrice : r.Price).ToList();

                    if (prices.Any())
                    {
                        hotel.MinPrice = prices.Min();
                        hotel.MaxPrice = prices.Max();
                        await _hotelRepository.UpdateAsync(hotel.Id, hotel);
                        await _hotelRepository.SaveChangesAsync();
                    }
                }
            }

            await _roomRepository.UpdateAsync(request.Id, room);
            await _roomRepository.SaveChangesAsync();

            var roomResponse = _mapper.Map<RoomResponse>(room);

            return Result.Success(roomResponse);
        }
    }
}