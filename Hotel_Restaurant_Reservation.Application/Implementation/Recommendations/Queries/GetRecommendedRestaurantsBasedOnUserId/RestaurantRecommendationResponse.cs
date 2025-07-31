using System.Text.Json.Serialization;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;

public class RestaurantRecommendationResponse
{
    [JsonPropertyName("recommended_business_ids")]
    public IEnumerable<string> RecommendedBusinessIds { get; set; }
}
