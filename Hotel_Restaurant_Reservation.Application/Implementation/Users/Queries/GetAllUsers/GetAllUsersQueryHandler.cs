using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore; // Add this using directive
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, Result<PagedResult<UserResponse>>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        // No longer need the Role repository, as we will use .Include()
        public GetAllUsersQueryHandler(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Get the base IQueryable.
                // I've also added .Include(u => u.Role) to efficiently load the related Role
                // in a single database query, which fixes the N+1 problem.
                var usersQuery = _userRepository.GetAllQuery()
                                                .Include(u => u.Role);

                // 2. Get the total count for pagination metadata.
                var totalCount = await usersQuery.CountAsync(cancellationToken);

                // 3. Apply pagination to the IQueryable.
                var pagedUsers = await usersQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                // 4. Map the results to the response DTO.
                var usersResponses = _mapper.Map<List<UserResponse>>(pagedUsers);

                // 5. Create the PagedResult object.
                var pagedResult = new PagedResult<UserResponse>(
                    usersResponses,
                    request.Page,
                    request.PageSize,
                    totalCount);

                return Result.Success(pagedResult);
            }
            catch (Exception ex)
            {
                return Result.Failure<PagedResult<UserResponse>>(
                    new Error("User.QueryError", $"An error occurred while retrieving users: {ex.Message}"));
            }
        }
    }
}
