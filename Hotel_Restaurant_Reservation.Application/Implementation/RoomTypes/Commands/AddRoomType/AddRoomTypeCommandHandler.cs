using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RoomTypes.Commands.AddRoomType;

public class AddRoomTypeCommandHandler : ICommandHandler<AddRoomTypeCommand, Result<RoomTypesResponse>>
{
    private readonly IGenericRepository<RoomType> _roomTypeRepository;

    public AddRoomTypeCommandHandler(IGenericRepository<RoomType> roomTypeRepository)
    {
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<Result<RoomTypesResponse>> Handle(AddRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var roomType = await _roomTypeRepository.GetFirstOrDefaultAsync(x => x.Description.ToLower() == request.NewRoomType.ToLower());

        if (roomType != null)
        {
            return Result.Failure<RoomTypesResponse>(DomainErrors.RoomType.ExistRoomType(roomType.Description));
        }

        roomType = new RoomType()
        {
            Id = Guid.NewGuid(),
            Description = request.NewRoomType,
        };

        await _roomTypeRepository.AddAsync(roomType);
        await _roomTypeRepository.SaveChangesAsync();

        var response = new RoomTypesResponse()
        {
            Id = roomType.Id,
            Description = roomType.Description,
        };
        return Result.Success(response);
    }
}
