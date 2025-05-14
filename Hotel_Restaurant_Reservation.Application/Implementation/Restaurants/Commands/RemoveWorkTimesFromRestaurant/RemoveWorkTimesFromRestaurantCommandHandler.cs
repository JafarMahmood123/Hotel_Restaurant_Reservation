using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;

public class RemoveWorkTimesFromRestaurantCommandHandler : ICommandHandler<RemoveWorkTimesFromRestaurantCommand, IEnumerable<WorkTime>>
{
    private readonly IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository;
    private readonly IGenericRepository<WorkTime> workTimeRepository;

    public RemoveWorkTimesFromRestaurantCommandHandler(IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository,
        IGenericRepository<WorkTime> workTimeRepository)
    {
        this.restaurantWorkTimeRepository = restaurantWorkTimeRepository;
        this.workTimeRepository = workTimeRepository;
    }

    public async Task<IEnumerable<WorkTime>> Handle(RemoveWorkTimesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var workTimeIds = request.WorkTimeIds;


        List<RestaurantWorkTime> restaurantWorkTimes = new List<RestaurantWorkTime>();
        foreach (var workTimeId in workTimeIds)
        {
            var restaurantWorkTime = await restaurantWorkTimeRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.WorkTimeId == workTimeId);

            restaurantWorkTimes.Add(restaurantWorkTime);
        }

        restaurantWorkTimeRepository.RemoveRange(restaurantWorkTimes);

        await restaurantWorkTimeRepository.SaveChangesAsync();

        List<WorkTime> workTimes = new List<WorkTime>();

        foreach (var workTimeId in workTimeIds)
        {
            workTimes.Add(await workTimeRepository.GetByIdAsync(workTimeId));
        }

        return workTimes;
    }
}
