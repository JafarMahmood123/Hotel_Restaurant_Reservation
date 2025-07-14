using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.DeleteCountry;

public class DeleteCountryCommandHandler : ICommandHandler<DeleteCountryCommand, Result>
{
    private readonly IGenericRepository<Country> _countryRepository;

    public DeleteCountryCommandHandler(IGenericRepository<Country> countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Result> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id);

        if (country is null)
        {
            return Result.Failure(DomainErrors.Country.NotFound(request.Id));
        }

        await _countryRepository.RemoveAsync(request.Id);
        await _countryRepository.SaveChangesAsync();

        return Result.Success();
    }
}