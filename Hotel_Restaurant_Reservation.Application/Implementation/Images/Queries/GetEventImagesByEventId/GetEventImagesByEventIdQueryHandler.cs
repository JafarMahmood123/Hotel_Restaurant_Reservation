using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetEventImages;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetEventImagesByEventId;

public class GetEventImagesQueryHandler : IQueryHandler<GetEventImagesByEventIdQuery, Result<List<string>>>
{
    private readonly IGenericRepository<EventImage> _eventImageRepository;
    private readonly IGenericRepository<Event> _eventRepository;

    public GetEventImagesQueryHandler(IGenericRepository<EventImage> eventImageRepository, IGenericRepository<Event> eventRepository)
    {
        _eventImageRepository = eventImageRepository;
        _eventRepository = eventRepository;
    }

    public async Task<Result<List<string>>> Handle(GetEventImagesByEventIdQuery request, CancellationToken cancellationToken)
    {
        var anEvent = await _eventRepository.GetByIdAsync(request.EventId);
        if (anEvent is null)
        {
            return Result.Failure<List<string>>(DomainErrors.Event.NotFound(request.EventId));
        }

        var images = await _eventImageRepository.Where(ei => ei.EventId == request.EventId).ToListAsync(cancellationToken);

        if (!images.Any())
        {
            return Result.Failure<List<string>>(DomainErrors.Event.NoImagesFound);
        }

        return Result.Success(images.Select(i => i.Url).ToList());
    }
}