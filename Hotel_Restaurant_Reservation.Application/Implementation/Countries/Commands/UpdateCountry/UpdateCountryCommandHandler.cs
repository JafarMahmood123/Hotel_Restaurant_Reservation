using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Commands.UpdateCountry;

public class UpdateCountryCommandHandler : ICommandHandler<UpdateCountryCommand, Country?>
{
    private readonly IGenericRepository<Country> genericRepository;

    public UpdateCountryCommandHandler(IGenericRepository<Country> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Country?> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await genericRepository.UpdateAsync(request.Id, request.Country);

        if (country is not null)
            await genericRepository.SaveChangesAsync();

        return country;
    }
}
