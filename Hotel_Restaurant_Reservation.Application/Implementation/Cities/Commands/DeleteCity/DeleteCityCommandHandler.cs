using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.DeleteCity;

public class DeleteCityCommandHandler : ICommandHandler<DeleteCityCommand, Result>
{
    private readonly IGenericRepository<City> _cityRepository;

    public DeleteCityCommandHandler(IGenericRepository<City> cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id);

        if (city is null)
        {
            return Result.Failure(DomainErrors.City.NotFound(request.Id));
        }

        await _cityRepository.RemoveAsync(request.Id);
        await _cityRepository.SaveChangesAsync();

        return Result.Success();
    }
}