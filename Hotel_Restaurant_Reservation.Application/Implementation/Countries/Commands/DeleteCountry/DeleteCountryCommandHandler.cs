using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.DeleteCountry;

public class DeleteCountryCommandHandler : ICommandHandler<DeleteCountryCommand, Country?>
{
    private readonly IGenericRepository<Country> genericRepository;

    public DeleteCountryCommandHandler(IGenericRepository<Country> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Country?> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await genericRepository.RemoveAsync(request.Id);

        if (country == null) return null;

        await genericRepository.SaveChangesAsync();

        return country;
    }
}
