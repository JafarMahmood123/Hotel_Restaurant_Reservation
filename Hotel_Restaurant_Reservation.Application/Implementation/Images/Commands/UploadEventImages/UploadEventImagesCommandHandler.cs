//using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
//using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
//using Hotel_Restaurant_Reservation.Application.Abstractions.Storage;
//using Hotel_Restaurant_Reservation.Domain.Entities;
//using Hotel_Restaurant_Reservation.Domain.Shared;

//namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Commands.UploadEventImage;

//public class UploadEventImagesCommandHandler : ICommandHandler<UploadEventImagesCommand, Result<List<string>>>
//{
//    private readonly IGenericRepository<Event> _eventRepository;
//    private readonly IGenericRepository<EventImage> _eventImageRepository;
//    private readonly IFileStorageService _fileStorageService;

//    public UploadEventImagesCommandHandler(
//        IGenericRepository<Event> eventRepository,
//        IGenericRepository<EventImage> eventImageRepository,
//        IFileStorageService fileStorageService)
//    {
//        _eventRepository = eventRepository;
//        _eventImageRepository = eventImageRepository;
//        _fileStorageService = fileStorageService;
//    }

//    public async Task<Result<List<string>>> Handle(UploadEventImagesCommand request, CancellationToken cancellationToken)
//    {
//        var anEvent = await _eventRepository.GetByIdAsync(request.EventId);
//        if (anEvent is null)
//        {
//            return Result.Failure<List<string>>(DomainErrors.Event.NotFound(request.EventId));
//        }

//        var uploadedUrls = new List<string>();

//        const string SUBFOLDER_NAME = "events";

//        foreach (var imageFile in request.ImageFiles)
//        {
//            var imageUrl = await _fileStorageService.SaveFileAsync(imageFile, SUBFOLDER_NAME);

//            var eventImage = new EventImage
//            {
//                Id = Guid.NewGuid(),
//                Url = imageUrl,
//                EventId = request.EventId
//            };

//            await _eventImageRepository.AddAsync(eventImage);
//            uploadedUrls.Add(imageUrl);
//        }

//        await _eventRepository.SaveChangesAsync();

//        return Result.Success(uploadedUrls);
//    }
//}