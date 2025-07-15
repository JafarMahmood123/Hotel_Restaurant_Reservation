using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries.GetAllTags
{
    public class GetAllTagsQueryHandler : IQueryHandler<GetAllTagsQuery, Result<IEnumerable<TagResponse>>>
    {
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public GetAllTagsQueryHandler(IGenericRepository<Tag> tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TagResponse>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _tagRepository.GetAllAsync();
            var tagResponses = _mapper.Map<IEnumerable<TagResponse>>(tags);
            return Result.Success(tagResponses);
        }
    }
}