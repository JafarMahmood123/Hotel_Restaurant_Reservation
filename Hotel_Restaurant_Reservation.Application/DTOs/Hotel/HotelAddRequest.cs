using Hotel_Restaurant_Reservation.Application.DTOs.CurrencyType;
using Hotel_Restaurant_Reservation.Application.DTOs.HotelRangePrices;
using Hotel_Restaurant_Reservation.Application.DTOs.Location;
using Hotel_Restaurant_Reservation.Application.DTOs.PropertyType;
using Hotel_Restaurant_Reservation.Application.DTOs.Room;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.DTOs.Hotel;

public class HotelAddRequest
{
    // Key Properties

    public string Name { get; set; }

    //public string Description { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Url { get; set; }

    public double StarRate { get; set; }

    public int NumberOfRooms { get; set; }

    // Additional Keys

    public PropertyTypeRequest PropertyType { get; set; }

    public IEnumerable<RoomRequest> Rooms { get; set; }

    public HotelRangePricesRequest HotelRangePrices { get; set; }

    public CurrencyTypeRequest CurrencyType { get; set; }

    public LocationRequest Location { get; set; }
}
