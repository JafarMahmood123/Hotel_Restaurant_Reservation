using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

public static class DomainErrors
{
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

        public static Error NotFound(Guid restaurantBookingId) => new(
            "RestaurantBooking.NotFound",
            $"The restaurant booking with the ID '{restaurantBookingId}' was not found.");

        public static Error DeletionNotAllowed(DateTime receiveTime) => new(
            "RestaurantBooking.DeletionNotAllowed",
            $"Dish deletion is not allowed at or after the delivery time, or within 15 minutes of the delivery time. Delivery time: {receiveTime:yyyy-MM-dd HH:mm}.");

        public static Error UpdateNotAllowed(DateTime receiveTime) => new(
            "RestaurantBooking.UpdateNotAllowed",
            $"Booking updates are not allowed at or after the delivery time, or within 15 minutes of the delivery time. Delivery time: {receiveTime:yyyy-MM-dd HH:mm}.");
    }

    public static class Payment
    {
        public static Error NotFound(Guid id) => new(
            "Payment.NotFound",
            $"Payment with ID {id} was not found.");

        public static Error InsufficientFunds() => new(
            "Payment.InsufficientFunds",
            $"Insufficient funds.");
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

        public static Error NoImagesProvided => new Error(
            "Restaurant.NoImagesProvided", "No images were provided.");

        public static Error NoImagesFound => new Error(
            "Restaurant.NoImagesFound", "No images were found for the restaurant.");

        public static Error AlreadyHaveCuisine => new Error(
            "Restaurant.AlreadyHaveCuisine",
            "This restaurant already have this cuisine.");

        public static Error AlreadyHaveMealType=> new Error(
            "Restaurant.AlreadyHaveMealType",
            "This restaurant already have this feature.");

        public static Error AlreadyHaveTag => new Error(
            "Restaurant.AlreadyHaveTag",
            "This restaurant already have this tag.");

        public static Error DishAlreadyExists => new Error(
            "Restaurant.DishAlreadyExists",
            "This restaurant already have this dish.");

        public static Error DontHaveDish => new Error(
           "Restaurant.DontHaveDish",
           "This restaurant don't have this dish.");
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

        public static Error NotFoundForFilters => new(
            "Hotel.NotFoundForFilters",
            "No hotels found matching the specified filters.");

        public static Error NoImagesProvided => new Error(
            "Hotel.NoImagesProvided", "No images were provided.");

        public static Error NoImagesFound => new Error(
            "Hotel.NoImagesFound", "No images were found for the hotel.");

        public static Error InvalidRequest => new(
            "Hotel.InvalidRequest",
            "The hotel request is invalid.");

        public static Error ExistingAmenity => new(
            "Hotel.ExistingAmenity",
            "The hotel already have this amenity.");

        public static Error DontHaveAmenity => new(
            "Hotel.DontHaveAmenity",
            "The hotel don't have this amenity.");
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

        public static Error NoImagesFound => new Error(
            "Event.NoImagesFound", "No images were found for the event.");
    }

    public static class Room
    {
        public static Error NotFound(Guid roomId) => new(
            "Room.NotFound",
            $"The room with the ID '{roomId}' was not found.");

        public static Error NumberAlreadyExists => new(
            "Room.NumberAlreadyExists",
            $"The room number is already assigned to another room.");
    }

    public static class RoomType
    {
        public static Error NotFound(Guid roomTypeId) => new(
            "RoomType.NotFound",
            $"The room type with the ID '{roomTypeId}' was not found.");

        public static Error ExistRoomType(string description) => new(
            "RoomType.ExistRoomType",
            $"The room type with the name {description} already exist.");
    }

    public static class EventReview
    {
        public static Error NotFound(Guid eventReviewId) => new(
            "EventReview.NotFound",
            $"The event review with the ID '{eventReviewId}' was not found.");
    }

    public static class HotelReview
    {
        public static Error NotFound(Guid hotelReviewId) => new(
            "HotelReview.NotFound",
            $"The hotel review with the ID '{hotelReviewId}' was not found.");
    }

    public static class BookingDishes
    {
        public static Error NotFound => new(
            "BookingDishes.NotFound",
            "No matching dishes found in the booking to remove.");

        public static Error SomeNotFound(IEnumerable<Guid> notFoundDishIds) => new(
            "BookingDishes.SomeNotFound",
            $"The following dishes were not found in the booking: {string.Join(", ", notFoundDishIds)}");

        public static Error InvalidQuantity(Guid dishId) => new(
            "BookingDishes.InvalidQuantity",
            $"Invalid quantity for dish with ID '{dishId}'. Quantity must be greater than zero.");

        public static Error Validation(IEnumerable<Error> errors)
        {
            var combinedErrorMessage = string.Join("; ", errors.Select(e => e.Message));
            return new Error("BookingDishes.Validation", combinedErrorMessage);
        }
    }

    public static class Country
    {
        public static Error ExistingCountry(string name) => new(
            "Country.ExistingCountry",
            $"A country with the name '{name}' already exists.");

        public static Error NotFound(Guid countryId) => new(
            "Country.NotFound",
            $"A country with the ID '{countryId}' was not found.");

        public static Error NotFoundByName(string name) => new(
            "Country.NotFoundByName",
            $"A country with the name '{name}' was not found.");

        public static Error SameName => new(
            "Country.SameName",
            "The new name is the same as the old name.");
    }

    public static class City
    {
        public static Error ExistingCity(string name, Guid countryId) => new(
            "City.ExistingCity",
            $"A city with the name '{name}' already exists in the country with ID '{countryId}'.");

        public static Error NotFound(Guid cityId) => new(
            "City.NotFound",
            $"A city with the ID '{cityId}' was not found.");

        public static Error NotFoundByName(string name) => new(
            "City.NotFoundByName",
            $"A city with the name '{name}' was not found.");

        public static Error SameName => new(
            "City.SameName",
            "The new name is the same as the old name.");
    }

    public static class LocalLocation
    {
        public static Error ExistingLocalLocation(string name) => new(
            "LocalLocation.ExistingLocalLocation",
            $"A local location with the name '{name}' already exists.");

        public static Error NotFound(Guid id) => new(
            "LocalLocation.NotFound",
            $"A local location with the ID '{id}' was not found.");

        public static Error NotFoundByName(string name) => new(
            "LocalLocation.NotFoundByName",
            $"A local location with the name '{name}' was not found.");

        public static Error SameName => new(
            "LocalLocation.SameName",
            "The new name is the same as the old name.");
    }

    public static class Location
    {
        public static Error NotFound(Guid locationId) => new(
            "Location.NotFound",
            $"The location with id = {locationId} is not found.");

        public static Error InvalidRequest => new(
            "Location.InvalidRequest",
            "The location request is invalid.");

        public static Error ExistingLocation => new(
            "Location.ExistingLocation",
            "This combination of country and city/local location already exists.");

        public static Error SameLocation => new(
            "Location.SameLocation",
            "The new location is the same as the old one.");
    }

    public static class CityLocalLocations
    {
        public static Error NotFound(Guid id) => new(
            "CityLocalLocations.NotFound",
            $"The CityLocalLocations with the ID '{id}' was not found.");
    }

    public static class User
    {
        public static Error SignUpExistingAccount(string email) => new(
            "User.SignUp.ExistingAccount",
            $"The email '{email}' is already associated with an existing account.");

        public static Error AddAdminExistingAccount(string email) => new(
            "User.AddAdmin.ExistingAccount",
            $"The email '{email}' is already associated with an existing account.");

        public static Error LogInUnExistingAccount(string email) => new(
            "User.LogIn.UnExistingAccount",
            $"No account found with email '{email}'.");

        public static Error IncorrectPassword() => new(
            "User.LogIn.IncorrectPassword",
            $"Incorrect password.");

        public static Error InvalidOldPassword() => new(
            "User.ChangePassword.InvalidOldPassword",
            "The old password you entered is incorrect.");

        public static Error NotFound(Guid userId) => new(
            "User.NotFound",
            $"The user with the ID '{userId}' was not found.");

        public static Error EmailInUse(string email) => new(
            "User.EmailInUse",
            $"The email '{email}' is already in use.");

        public static Error NoImagesProvided => new Error(
            "User.NoImagesProvided", "No images were provided.");

        public static Error NoImagesFound => new Error(
            "User.NoImagesFound", "No images were found for the user.");

    }

    public static class Role
    {
        public static Error NotFound(Guid roleId) => new(
            "Role.NotFound",
            $"The role with the ID '{roleId}' was not found.");

        public static Error CustomerRoleNotFound() => new(
            "Role.CustomerRoleNotFound",
            "The default 'Customer' role was not found in the database.");

        public static Error AdminRoleNotFound() => new(
            "Role.AdminRoleNotFound",
            "The default 'Admin' role was not found in the database.");
    }

    public static class Cuisine
    {
        public static Error NotExistCuisine(Guid cuisineId) => new(
            "Cuisine.GetCuisine.NotExistCuisine",
            $"Cuisine with ID '{cuisineId}' does not exist.");

        public static Error ExistingCuisine(string cuisineName) => new(
            "Cuisine.AddCuisine.ExistingCuisine",
            $"A cuisine with the name '{cuisineName}' already exists.");

        public static Error NotFound(Guid cuisineId) => new(
            "Cuisine.NotFound",
            $"The cuisine with the ID '{cuisineId}' was not found.");
    }

    public static class CurrencyType
    {
        public static Error NotFound(Guid currencyTypeId) => new(
            "CurrencyType.NotFound",
            $"Currency type with ID {currencyTypeId} was not found.");

        public static Error NotFound(string currencyCode) => new(
            "CurrencyType.GetCurrencyType.NotFound",
            $"Currency type with code '{currencyCode}' does not exist.");

        public static Error SameCurrencyCode => new(
            "CurrencyType.SameCurrencyCode",
            "The new currency code is the same as the old one.");

        public static Error ExistingCurrencyType(string currencyCode) => new(
            "CurrencyType.ExistingCurrencyType",
            $"A currency type with the code '{currencyCode}' already exists.");
    }

    public static class HotelReservation
    {
        public static Error NotFound(Guid hotelReservationId) => new(
            "HotelReservation.NotFound",
            $"The hotel reservation with the ID '{hotelReservationId}' was not found.");

        public static Error InvalidNumberOfPeople() => new(
            "HotelReservation.InvalidNumberOfPeople",
            $"The hotel reservation has invalid number of people.");

        public static Error UpdateNotAllowed() => new(
            "HotelReservation.UpdateNotAllowed",
            "Hotel reservations cannot be updated.");

        public static Error UpdateNotAllowedPastReservation() => new(
            "HotelReservation.UpdateNotAllowedPastReservation",
            "Cannot update a reservation that has already ended.");

        public static Error DeleteNotAllowedPastReservation() => new(
            "HotelReservation.DeleteNotAllowedPastReservation",
            "Cannot delete a reservation that has already ended.");
    }
}