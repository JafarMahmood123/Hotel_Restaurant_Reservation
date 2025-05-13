using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Commands.AddWorkTime;

public class AddWorkTimeCommandHandler : ICommandHandler<AddWorkTimeCommand, WorkTime>
{
    private readonly IGenericRepository<WorkTime> _genericRepository;

    public AddWorkTimeCommandHandler(IGenericRepository<WorkTime> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<WorkTime> Handle(AddWorkTimeCommand request, CancellationToken cancellationToken)
    {
        WorkTime workTime = request.WorkTime;

        workTime = await _genericRepository.AddAsync(workTime);

        await _genericRepository.SaveChangesAsync();

        return workTime;
    }
}
