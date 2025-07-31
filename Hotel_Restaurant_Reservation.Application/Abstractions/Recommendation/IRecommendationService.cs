namespace Hotel_Restaurant_Reservation.Application.Abstractions.Recommendation;

public interface IRecommendationService
{
    public Task<IEnumerable<string>> GetRecommendations(string userId);
}
