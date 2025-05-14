using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

public class AddWorkTimesToRestaurantCommandHandler : ICommandHandler<AddWorkTimesToRestaurantCommand, IEnumerable<WorkTime>>
{
    private readonly IGenericRepository<WorkTime> workTimeRepository;
    private readonly IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository;

    public AddWorkTimesToRestaurantCommandHandler(IGenericRepository<WorkTime> workTimeRepository,
        IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository)
    {
        this.workTimeRepository = workTimeRepository;
        this.restaurantWorkTimeRepository = restaurantWorkTimeRepository;
    }

    public async Task<IEnumerable<WorkTime>> Handle(AddWorkTimesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var workTimeIds = request.WorkTimeIds;

        //ToDo...
        //Manage the intersection between worktimes

        List<RestaurantWorkTime> restaurantWorkTimes = new List<RestaurantWorkTime>();

        foreach (var workTimeId in workTimeIds)
        {
            var restaurantWorkTime = await restaurantWorkTimeRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.WorkTimeId == workTimeId);

            if (restaurantWorkTime == null)
            {

                restaurantWorkTime = new RestaurantWorkTime()
                {
                    Id = Guid.NewGuid(),
                    WorkTimeId = workTimeId,
                    RestaurantId = restaurantId
                };

                restaurantWorkTimes.Add(restaurantWorkTime);
            }

        }

        await restaurantWorkTimeRepository.AddRangeAsync(restaurantWorkTimes);

        await restaurantWorkTimeRepository.SaveChangesAsync();


        List<WorkTime> workTimes = new List<WorkTime>();

        foreach (var workTimeId in workTimeIds)
        {
            workTimes.Add(await workTimeRepository.GetByIdAsync(workTimeId));
        }

        return workTimes;
    }
}
