using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddWorkTimesToRestaurant;

public class AddWorkTimesToRestaurantCommandHandler
    : ICommandHandler<AddWorkTimesToRestaurantCommand, Result<List<WorkTimeResponse>>>
{
    private readonly IGenericRepository<WorkTime> _workTimeRepository;
    private readonly IGenericRepository<RestaurantWorkTime> _restaurantWorkTimeRepository;
    private readonly IMapper _mapper;

    public AddWorkTimesToRestaurantCommandHandler(
        IGenericRepository<WorkTime> workTimeRepository,
        IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository,
        IMapper mapper)
    {
        _workTimeRepository = workTimeRepository;
        _restaurantWorkTimeRepository = restaurantWorkTimeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<WorkTimeResponse>>> Handle(
    AddWorkTimesToRestaurantCommand request,
    CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var workTimeIds = request.AddWorkTimesToRestaurantRequest.Ids;

        // Get all work times to be added
        var newWorkTimes = new List<WorkTime>();
        foreach (var workTimeId in workTimeIds)
        {
            var workTime = await _workTimeRepository.GetByIdAsync(workTimeId);
            if (workTime == null)
            {
                return Result.Failure<List<WorkTimeResponse>>(
                    DomainErrors.WorkTime.NotFound(workTimeId));
            }
            newWorkTimes.Add(workTime);
        }

        // Get existing work times for this restaurant
        var existingWorkTimes = await GetExistingWorkTimes(restaurantId);

        // Process time conflicts by merging and getting work times to delete
        var (mergedWorkTimes, workTimesToDelete) = ProcessWorkTimeConflicts(newWorkTimes, existingWorkTimes);

        // Delete conflicting work times
        if (workTimesToDelete.Any())
        {
            var associationsToDelete = await _restaurantWorkTimeRepository
                .Where(x => x.RestaurantId == restaurantId &&
                       workTimesToDelete.Select(wt => wt.Id).Contains(x.WorkTimeId))
                .ToListAsync();

            _restaurantWorkTimeRepository.RemoveRange(associationsToDelete);
        }

        // Create new merged work times and associations
        var newWorkTimeEntities = new List<WorkTime>();
        var newAssociations = new List<RestaurantWorkTime>();

        foreach (var mergedWorkTime in mergedWorkTimes)
        {
            // Check if this is a new merged work time (not existing in DB)
            if (!existingWorkTimes.Any(wt =>
                wt.Day == mergedWorkTime.Day &&
                wt.OpenHour == mergedWorkTime.OpenHour &&
                wt.CloseHour == mergedWorkTime.CloseHour))
            {
                // Create new work time entity
                var newWorkTime = new WorkTime
                {
                    Id = Guid.NewGuid(),
                    Day = mergedWorkTime.Day,
                    OpenHour = mergedWorkTime.OpenHour,
                    CloseHour = mergedWorkTime.CloseHour
                };
                newWorkTimeEntities.Add(newWorkTime);

                // Create association
                newAssociations.Add(new RestaurantWorkTime
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = restaurantId,
                    WorkTimeId = newWorkTime.Id
                });
            }
            else
            {
                // Use existing work time
                var existingWorkTime = existingWorkTimes.First(wt =>
                    wt.Day == mergedWorkTime.Day &&
                    wt.OpenHour == mergedWorkTime.OpenHour &&
                    wt.CloseHour == mergedWorkTime.CloseHour);

                newAssociations.Add(new RestaurantWorkTime
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = restaurantId,
                    WorkTimeId = existingWorkTime.Id
                });
            }
        }

        // Save changes in transaction
        if (newWorkTimeEntities.Any())
        {
            await _workTimeRepository.AddRangeAsync(newWorkTimeEntities);
        }
        if (newAssociations.Any())
        {
            await _restaurantWorkTimeRepository.AddRangeAsync(newAssociations);
        }
        await _restaurantWorkTimeRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<List<WorkTimeResponse>>(mergedWorkTimes);
        return Result.Success(response);
    }

    private async Task<List<WorkTime>> GetExistingWorkTimes(Guid restaurantId)
    {
        var restaurantWorkTimes = _restaurantWorkTimeRepository.Where(
            x => x.RestaurantId == restaurantId)
            .Include(x => x.WorkTime);

        return await restaurantWorkTimes.Select(x => x.WorkTime).ToListAsync();
    }

    private (List<WorkTime> mergedWorkTimes, List<WorkTime> workTimesToDelete) ProcessWorkTimeConflicts(
    List<WorkTime> newWorkTimes,
    List<WorkTime> existingWorkTimes)
    {
        var allWorkTimes = existingWorkTimes.Concat(newWorkTimes).ToList();
        var mergedWorkTimes = new List<WorkTime>();
        var workTimesToDelete = new List<WorkTime>();

        // Group by day and process each day separately
        foreach (var dayGroup in allWorkTimes.GroupBy(x => x.Day))
        {
            var dayWorkTimes = dayGroup.OrderBy(x => x.OpenHour).ToList();
            var currentMerged = dayWorkTimes.First();

            for (int i = 1; i < dayWorkTimes.Count; i++)
            {
                if (dayWorkTimes[i].OpenHour <= currentMerged.CloseHour)
                {
                    // Mark overlapping work times for deletion
                    workTimesToDelete.Add(dayWorkTimes[i]);

                    // Expand the merged time
                    currentMerged.CloseHour = dayWorkTimes[i].CloseHour > currentMerged.CloseHour
                        ? dayWorkTimes[i].CloseHour
                        : currentMerged.CloseHour;
                }
                else
                {
                    mergedWorkTimes.Add(currentMerged);
                    currentMerged = dayWorkTimes[i];
                }
            }
            mergedWorkTimes.Add(currentMerged);
        }

        // Only keep work times that were merged (remove originals)
        workTimesToDelete = workTimesToDelete
            .Where(wt => !mergedWorkTimes.Any(m =>
                m.Day == wt.Day &&
                m.OpenHour == wt.OpenHour &&
                m.CloseHour == wt.CloseHour))
            .ToList();

        return (mergedWorkTimes, workTimesToDelete);
    }
}