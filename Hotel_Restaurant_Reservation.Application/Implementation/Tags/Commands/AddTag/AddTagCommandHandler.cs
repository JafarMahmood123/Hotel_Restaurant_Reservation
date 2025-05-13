using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Tags.Commands.AddTag;

public class AddTagCommandHandler : ICommandHandler<AddTagCommand, Tag>
{
    private readonly IGenericRepository<Tag> _genericRepository;

    public AddTagCommandHandler(IGenericRepository<Tag> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Tag> Handle(AddTagCommand request, CancellationToken cancellationToken)
    {
        Tag tag = request.Tag;

        tag = await _genericRepository.AddAsync(tag);

        await _genericRepository.SaveChangesAsync();

        return tag;
    }
}
