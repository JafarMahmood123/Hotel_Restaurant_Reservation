using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Tags.Commands.AddTag;

public class AddTagCommand : ICommand<Result<TagResponse>>
{
    public AddTagCommand(AddTagRequest addTagRequest)
    {
        AddTagRequest = addTagRequest;
    }

    public AddTagRequest AddTagRequest { get; }
}