using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Tags.Commands.AddTag;

public class AddTagCommand : ICommand<Tag>
{
    public Tag Tag { get; set; }

    public AddTagCommand(Tag tag)
    {
        Tag = tag;
    }
}
