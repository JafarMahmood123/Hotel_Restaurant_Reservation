using Hotel_Restaurant_Reservation.Application.DTOs.CityDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.MealTypeDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.TagDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.WorkTimeDTOs;

namespace Hotel_Restaurant_Reservation.Application.DTOs.RestaurantDTOs;

public class RestaurantAddRequest
{

    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public string PictureUrl { get; set; }

    public double StarRating { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int NumberOfTables { get; set; }

    public string PriceLevel { get; set; }

    //public IEnumerable<WorkTimeRequest> WorkTimeRequests { get; set; }

    //public IEnumerable<FeatureRequest> FeatureRequests { get; set; }

    //public IEnumerable<TagRequest> TagRequests { get; set; }

    //public IEnumerable<CuisineRequest> CuisineRequests { get; set; }

    //public IEnumerable<DishRequest> DishRequests { get; set; }

    //public IEnumerable<MealTypeRequest> MealTypeRequests { get; set; }

    public AddCountryRequest CountryRequest { get; set; }

    public AddCityRequest CityRequest { get; set; }

    public LocalLocationRequest LocalLocationRequest { get; set; }
}
