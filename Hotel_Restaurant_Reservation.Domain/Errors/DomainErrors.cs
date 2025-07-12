using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

public static class DomainErrors
{
    public static class Customer
    {
        public static Error SignUpExistingAccount(string email) => new(
            "Customer.SignUp.ExistingAccount",
            $"The email '{email}' is already associated with an existing account.");

        public static Error LogInUnExistingAccount(string email) => new(
            "Customer.LogIn.UnExistingAccount",
            $"No account found with email '{email}'.");

        public static Error IncorrectPassword() => new(
            "Customer.LogIn.IncorrectPassword",
            $"Incorrect password.");
    }

    public static class Dish
    {
        public static Error ExistingDish(string dishName) => new(
            "Dish.AddDish.ExistingDish",
            $"A dish with the name '{dishName}' already exists.");

        public static Error NotFound(Guid dishId) => new(
            "Dish.GetDish.NotFound",
            $"A dish with the Id '{dishId}' does not exist.");
    }

    public static class Feature
    {
        public static Error ExistingFeature(string featureName) => new(
            "Feature.AddFeature.ExistingFeature",
            $"A feature with the name '{featureName}' already exists.");

        public static Error NotFound(Guid featureId) => new(
            "Feature.NotFound",
            $"Feature with ID {featureId} was not found.");
    }

    public static class MealType
    {
        public static Error ExistingMealType(string mealTypeName) => new(
            "MealType.AddMealType.ExistingMealType",
            $"A meal type with the name '{mealTypeName}' already exists.");

        public static Error NotFound(Guid mealTypeId) => new(
            "MealType.GetMealType.NotFound",
            $"The meal type with ID '{mealTypeId}' does not exist.");
    }

    public static class RestaurantBooking
    {
        public static Error BookedTableAtThisTime(int tableNumber, DateTime time) => new(
            "RestaurantBooking.AddRestaurantBooking.BookedTableAtThisTime",
            $"Table {tableNumber} is already booked at {time:yyyy-MM-dd HH:mm}.");

        public static Error ShortBookingTime() => new(
            "RestaurantBooking.AddRestaurantBooking.ShortBookingTime",
            $"Booking duration must be at least 15 minutes.");

        public static Error LongBookingTime() => new(
            "RestaurantBooking.AddRestaurantBooking.LongBookingTime",
            $"Booking duration cannot exceed 60 minutes.");
    }

    public static class RestaurantReview
    {
        public static Error EmptyDescription => new(
            "RestaurantReview.AddReview.EmptyDescription",
            "Review description cannot be empty.");

        public static Error RatingLessThanOne => new(
            "RestaurantReview.AddReview.RatingLessThanOne",
            "Rating must be at least 1 star.");

        public static Error RatingGreaterThanFive => new(
            "RestaurantReview.AddReview.RatingGreaterThanFive",
            "Rating cannot exceed 5 stars.");
    }

    public static class Cuisine
    {
        public static Error NotExistCuisine(Guid cuisineId) => new(
            "Cuisine.GetCuisine.NotExistCuisine",
            $"Cuisine with ID '{cuisineId}' does not exist.");
    }

    public static class CurrencyType
    {
        public static Error NotFound(Guid currencyTypeId) => new(
            "CurrencyType.NotFound",
            $"Currency type with ID {currencyTypeId} was not found.");

        public static Error NotFound(string currencyCode) => new(
            "CurrencyType.GetCurrencyType.NotFound",
            $"Currency type with code '{currencyCode}' does not exist.");
    }

    public static class Restaurant
    {
        public static Error InvalidRequest => new(
            "Restaurant.InvalidRequest",
            "The restaurant request is invalid.");

        public static Error InvalidStarRating => new(
            "Restaurant.InvalidStarRating",
            "Star rating must be between 1 and 5.");

        public static Error InvalidTableCount => new(
            "Restaurant.InvalidTableCount",
            "Number of tables must be greater than 0.");

        public static Error DuplicateName => new(
            "Restaurant.DuplicateName",
            "A restaurant with this name already exists.");

        public static Error NotFound(Guid id) => new(
            "Restaurant.NotFound",
            $"Restaurant with ID {id} was not found.");

        public static Error NoCuisinesToRemove => new(
            "Restaurant.NoCuisinesToRemove",
            "No matching cuisine associations found to remove.");

        public static Error NoCurrencyTypesToRemove => new(
            "Restaurant.NoCurrencyTypesToRemove",
            "No matching currency type associations found to remove.");

        public static Error NoDishesToRemove => new(
            "Restaurant.NoDishesToRemove",
            "No matching dish associations found to remove.");

        public static Error NoFeaturesToRemove => new(
           "Restaurant.NoFeaturesToRemove",
           "No matching feature associations found to remove.");

        public static Error NoMealTypesToRemove => new(
            "Restaurant.NoMealTypesToRemove",
            "No matching meal type associations found to remove.");

        public static Error NoTagsToRemove => new(
            "Restaurant.NoTagsToRemove",
            "No matching tag associations found to remove.");

        public static Error NoWorkTimesToRemove => new(
            "Restaurant.NoWorkTimesToRemove",
            "No matching work time associations found to remove.");

        public static Error NotFoundForFilters => new(
            "Restaurant.NotFoundForFilters",
            "No restaurants found matching the specified filters.");

        public static Error NoDishesWithPricesProvided => new(
        "Restaurant.NoDishesWithPricesProvided",
        "At least one dish with price must be provided.");
    }

    public static class Location
    {
        public static Error NotFound(Guid locationId) => new(
            "Location.NotFound",
            $"The location with id = {locationId} is not found.");

        public static Error InvalidRequest => new(
            "Location.InvalidRequest",
            "The location request is invalid.");
    }

    public static class Tag
    {
        public static Error NotFound(Guid tagId) => new(
            "Tag.NotFound",
            $"Tag with ID {tagId} was not found.");

        public static Error ExistingTag(string tagName) => new(
            "Tag.ExistingTag",
            $"Tag with name '{tagName}' already exists.");
    }

    public static class WorkTime
    {
        public static Error NotFound(Guid workTimeId) => new(
            "WorkTime.NotFound",
            $"Work time with ID {workTimeId} was not found.");

        public static Error ExistingWorkTime() => new(
            "WorkTime.ExistingWorkTime",
            $"Work time already exists.");
    }

    public static class Hotel
    {
        public static Error NotFound(Guid id) => new(
            "Hotel.NotFound",
            $"Hotel with ID {id} was not found.");
    }

    public static class PropertyType
    {
        public static Error NotFound(Guid propertyTypeId) => new(
            "PropertyType.NotFound",
            $"The property type with the ID '{propertyTypeId}' was not found.");

        public static Error NotFound(string name) => new(
            "PropertyType.NotFound",
            $"PropertyType with the Name {name} was not found.");
    }

    public static class Amenity
    {
        public static Error ExistingAmenity(string amenityName) => new(
            "Amenity.AddAmenity.ExistingAmenity",
            $"An amenity with the name '{amenityName}' already exists.");

        public static Error NotFound(Guid amenityId) => new(
            "Amenity.GetAmenity.NotFound",
            $"An amenity with the Id '{amenityId}' does not exist.");
    }

    public static class EventRegistration
    {
        public static Error NotFound(Guid eventRegistrationId) => new(
            "EventRegistration.NotFound",
            $"The event registration with the ID '{eventRegistrationId}' was not found.");
    }

    public static class Event
    {
        public static Error NotFound(Guid eventId) => new(
            "Event.NotFound",
            $"The event with the ID '{eventId}' was not found.");
    }

    public static class HotelReservation
    {
        public static Error NotFound(Guid hotelReservationId) => new(
            "HotelReservation.NotFound",
            $"The hotel reservation with the ID '{hotelReservationId}' was not found.");
    }
}