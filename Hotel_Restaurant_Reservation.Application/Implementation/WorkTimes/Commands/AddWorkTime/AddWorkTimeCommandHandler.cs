using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Commands.AddWorkTime;

public class AddWorkTimeCommandHandler : ICommandHandler<AddWorkTimeCommand, Result<WorkTimeResponse>>
{
    private readonly IGenericRepository<WorkTime> _workTimeRepository;
    private readonly IMapper _mapper;

    public AddWorkTimeCommandHandler(
        IGenericRepository<WorkTime> workTimeRepository,
        IMapper mapper)
    {
        _workTimeRepository = workTimeRepository;
        _mapper = mapper;
    }

    public async Task<Result<WorkTimeResponse>> Handle(
        AddWorkTimeCommand request,
        CancellationToken cancellationToken)
    {
        var workTime = _mapper.Map<WorkTime>(request.AddWorkTimeRequest);

        var existingWorkTime = await _workTimeRepository.GetFirstOrDefaultAsync(
            x => x.Day == workTime.Day && workTime.OpenHour == x.OpenHour && workTime.CloseHour== x.CloseHour);

        if (existingWorkTime == null)
        {
            workTime.Id = Guid.NewGuid();

            workTime = await _workTimeRepository.AddAsync(workTime);
            await _workTimeRepository.SaveChangesAsync();

            return Result.Success(_mapper.Map<WorkTimeResponse>(workTime));
        }

        workTime.Id = Guid.NewGuid();
        workTime = await _workTimeRepository.AddAsync(workTime);
        await _workTimeRepository.SaveChangesAsync();

        var response = _mapper.Map<WorkTimeResponse>(workTime);
        return Result.Success(response);
    }
}