using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries.GetAllCuisines
{
    public class GetAllCuisinesQueryHandler : IQueryHandler<GetAllCuisinesQuery, Result<IEnumerable<CuisineResponse>>>
    {
        private readonly IGenericRepository<Cuisine> _cuisineRepository;
        private readonly IMapper _mapper;

        public GetAllCuisinesQueryHandler(IGenericRepository<Cuisine> cuisineRepository, IMapper mapper)
        {
            _cuisineRepository = cuisineRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CuisineResponse>>> Handle(GetAllCuisinesQuery request, CancellationToken cancellationToken)
        {
            var cuisines = await _cuisineRepository.GetAllAsync();
            var cuisineResponses = _mapper.Map<IEnumerable<CuisineResponse>>(cuisines);
            return Result.Success(cuisineResponses);
        }
    }
}