using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Commands.AddWorkTime;

public class AddWorkTimeCommand : ICommand<Result<WorkTimeResponse>>
{
    public AddWorkTimeCommand(AddWorkTimeRequest addWorkTimeRequest)
    {
        AddWorkTimeRequest = addWorkTimeRequest;
    }

    public AddWorkTimeRequest AddWorkTimeRequest { get; }
}