using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantImagesByRestaurantId;

public class GetRestaurantImagesByRestaurantIdQueryHandler : IQueryHandler<GetRestaurantImagesByRestaurantIdQuery, Result<List<string>>>
{
    private readonly IGenericRepository<RestaurantImage> _restaurantImageRepository;
    private readonly IGenericRepository<Restaurant> _restaurantRepository;

    public GetRestaurantImagesByRestaurantIdQueryHandler(IGenericRepository<RestaurantImage> restaurantImageRepository, IGenericRepository<Restaurant> restaurantRepository)
    {
        _restaurantImageRepository = restaurantImageRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<Result<List<string>>> Handle(GetRestaurantImagesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
        {
            return Result.Failure<List<string>>(DomainErrors.Restaurant.NotFound(request.RestaurantId));
        }

        var images = await _restaurantImageRepository.Where(ei => ei.RestaurantId == request.RestaurantId).ToListAsync(cancellationToken);

        if (!images.Any())
        {
            return Result.Failure<List<string>>(DomainErrors.Restaurant.NoImagesFound);
        }

        return Result.Success(images.Select(i => i.Url).ToList());
    }
}