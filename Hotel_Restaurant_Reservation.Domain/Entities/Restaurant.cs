using System.Xml.Serialization;

namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Restaurant
{
    // Key Properties
    public Guid Id { get; set; }  

    public string Name { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public string PictureUrl { get; set; }

    public double StarRating { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int NumberOfTables { get; set; }

    public string PriceLevel { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }

    public Guid RestaurantCurrencyTypeId { get; set; }


    // Navigation Properties

    public virtual ICollection<RestaurantWorkTime> RestaurantWorkTimes { get; set; }

    public virtual ICollection<RestaurantFeature> RestaurantFeatures { get; set; }

    public virtual ICollection<RestaurantTag> RestaurantTags { get; set; }

    public virtual ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }

    public virtual ICollection<RestaurantDishPrice> RestaurantDishPrices { get; set; }

    public virtual ICollection<RestaurantMealType> RestaurantMealTypes { get; set; }

    public virtual ICollection<RestaurantReview> RestaurantReviews { get; set; }

    public virtual ICollection<RestaurantCurrencyTypes> RestaurantCurrencyTypes { get; set; }

    public virtual Location Location { get; set; }

    public virtual ICollection<RestaurantBooking> RestaurantBookings { get; set; }


    public Restaurant()
    {
        RestaurantWorkTimes = new HashSet<RestaurantWorkTime>();
        RestaurantFeatures = new HashSet<RestaurantFeature>();
        RestaurantTags = new HashSet<RestaurantTag>();
        RestaurantCuisines = new HashSet<RestaurantCuisine>();
        RestaurantDishPrices = new HashSet<RestaurantDishPrice>();
        RestaurantMealTypes = new HashSet<RestaurantMealType>();
        RestaurantBookings = new HashSet<RestaurantBooking>();
        RestaurantReviews = new HashSet<RestaurantReview>();
    }
}
