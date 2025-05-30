using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Tags.Commands.AddTag;

public class AddTagCommandHandler : ICommandHandler<AddTagCommand, Result<TagResponse>>
{
    private readonly IGenericRepository<Tag> tagRepository;
    private readonly IMapper _mapper;

    public AddTagCommandHandler(
        IGenericRepository<Tag> tagRepository,
        IMapper mapper)
    {
        this.tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<Result<TagResponse>> Handle(
        AddTagCommand request,
        CancellationToken cancellationToken)
    {
        var tag = _mapper.Map<Tag>(request.AddTagRequest);

        var existingTag = await tagRepository.GetFirstOrDefaultAsync(x => x.Name == tag.Name);

        if (existingTag != null)
        {
            return Result.Failure<TagResponse>(
                DomainErrors.Tag.ExistingTag(tag.Name));
        }

        tag.Id = Guid.NewGuid();
        tag = await tagRepository.AddAsync(tag);
        await tagRepository.SaveChangesAsync();

        var tagResponse = _mapper.Map<TagResponse>(tag);
        return Result.Success(tagResponse);
    }
}