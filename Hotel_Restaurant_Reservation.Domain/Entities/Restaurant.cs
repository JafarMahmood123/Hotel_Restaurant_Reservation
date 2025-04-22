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

    // Foreign Keys



    // Navigation Properties

    public virtual RestaurantRangePrices RangePrice { get; set; }

    public virtual PriceLevel PriceLevel { get; set; }

    public virtual ICollection<WorkTime> WorkTimes { get; set; }

    public virtual ICollection<Feature> Features { get; set; }

    public virtual ICollection<Tag> Tags { get; set; }

    public virtual ICollection<Cuisine> Cuisines { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; }

    public virtual ICollection<MealType> MealTypes { get; set; }

    public virtual ICollection<RestaurantOrder> RestaurantOrders { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }

    public virtual Location Location { get; set; }

    public Restaurant()
    {
        WorkTimes = new HashSet<WorkTime>();
        Features = new HashSet<Feature>();
        Tags = new HashSet<Tag>();
        Cuisines = new HashSet<Cuisine>();
        Dishes = new HashSet<Dish>();
        MealTypes = new HashSet<MealType>();
        RestaurantOrders = new HashSet<RestaurantOrder>();
    }
}
