using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetUserImagesByUserId;

public class GetUserImagesByUserIdQueryHandler : IQueryHandler<GetUserImagesByUserIdQuery, Result<List<string>>>
{
    private readonly IGenericRepository<UserImage> _userImageRepository;
    private readonly IGenericRepository<User> _userRepository;

    public GetUserImagesByUserIdQueryHandler(IGenericRepository<UserImage> userImageRepository, IGenericRepository<User> userRepository)
    {
        _userImageRepository = userImageRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<List<string>>> Handle(GetUserImagesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return Result.Failure<List<string>>(DomainErrors.User.NotFound(request.UserId));
        }

        var images = await _userImageRepository.Where(ui => ui.UserId == request.UserId).ToListAsync(cancellationToken);

        if (!images.Any())
        {
            return Result.Failure<List<string>>(DomainErrors.User.NoImagesFound);
        }

        return Result.Success(images.Select(i => i.Url).ToList());
    }
}