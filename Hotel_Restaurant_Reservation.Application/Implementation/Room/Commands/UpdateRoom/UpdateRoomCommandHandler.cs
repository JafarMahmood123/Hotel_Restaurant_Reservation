using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : ICommandHandler<UpdateRoomCommand, Result<RoomResponse>>
    {
        private readonly IGenericRepository<Room> _roomRepository;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IGenericRepository<Room> roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomResponse>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(request.Id);

            if (room is null)
            {
                return Result.Failure<RoomResponse>(DomainErrors.Room.NotFound(request.Id));
            }

            _mapper.Map(request.UpdateRoomRequest, room);

            await _roomRepository.UpdateAsync(request.Id, room);
            await _roomRepository.SaveChangesAsync();

            var roomResponse = _mapper.Map<RoomResponse>(room);

            return Result.Success(roomResponse);
        }
    }
}