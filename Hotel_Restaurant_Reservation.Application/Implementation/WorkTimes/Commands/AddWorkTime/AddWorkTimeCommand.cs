using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Commands.AddWorkTime;

public class AddWorkTimeCommand : ICommand<WorkTime>
{
    public WorkTime WorkTime { get; set; }

    public AddWorkTimeCommand(WorkTime workTime)
    {
        WorkTime = workTime;
    }
}
