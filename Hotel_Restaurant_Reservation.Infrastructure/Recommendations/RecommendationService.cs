using Hotel_Restaurant_Reservation.Application.Abstractions.Recommendation;
using Hotel_Restaurant_Reservation.Application.Implementation.Recommendations.Queries.GetRecommendedRestaurantsBasedOnUserId;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hotel_Restaurant_Reservation.Infrastructure.Recommendations;

public class RecommendationService : IRecommendationService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseRecommendationUrl = "https://0445d2146f40.ngrok-free.app";

    public RecommendationService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IEnumerable<string>> GetRecommendations(string userId)
    {
        var requestUrl = $"{_baseRecommendationUrl}/recommendations/{userId}";

        try
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();


            var recommendationObject = JsonSerializer.Deserialize<RestaurantRecommendationResponse>(
                jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return recommendationObject?.RecommendedBusinessIds ?? new List<string>();
        }
        catch (HttpRequestException ex)
        {
            return new List<string>();
        }
        catch (JsonException ex)
        {
            return new List<string>();
        }
    }
}