using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.AddCuisine
{
    public class AddCuisineCommandHandler : ICommandHandler<AddCuisineCommand, Result<CuisineResponse>>
    {
        private readonly IGenericRepository<Cuisine> _cuisineRepository;
        private readonly IMapper _mapper;

        public AddCuisineCommandHandler(IGenericRepository<Cuisine> cuisineRepository, IMapper mapper)
        {
            _cuisineRepository = cuisineRepository;
            _mapper = mapper;
        }

        public async Task<Result<CuisineResponse>> Handle(AddCuisineCommand request, CancellationToken cancellationToken)
        {
            var existingCuisine = await _cuisineRepository.GetFirstOrDefaultAsync(c => c.Name == request.Name);
            if (existingCuisine != null)
            {
                return Result.Failure<CuisineResponse>(DomainErrors.Cuisine.ExistingCuisine(request.Name));
            }

            var cuisine = new Cuisine
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await _cuisineRepository.AddAsync(cuisine);
            await _cuisineRepository.SaveChangesAsync();

            var cuisineResponse = _mapper.Map<CuisineResponse>(cuisine);
            return Result.Success(cuisineResponse);
        }
    }
}