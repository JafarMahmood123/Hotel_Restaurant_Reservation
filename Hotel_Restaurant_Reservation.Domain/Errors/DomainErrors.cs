using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Domain.Errors;

public static class DomainErrors
{
    public static class Customer
    {
        public static readonly Error SignUpExistingAccount = new Error(
            "Customer.SignUp.ExistingAccount",
            "Your are trying to signup to an existing account.");
        
        public static readonly Error LogInUnExistingAccount = new Error(
            "Customer.LogIn.UnExistingAccount",
            "Your are trying to login to an unexisting account.");

        public static readonly Error IncorrectPassword = new Error(
            "Customer.LogIn.IncorrectPassword",
            "Your password is incorrect.");
    }

    public static class Dish
    {
        public static readonly Error ExistingDish = new Error(
            "Dish.AddDish.ExistingDish",
            "The dish is already existing.");
    }

    public static class Feature
    {
        public static readonly Error ExistingFeature= new Error(
            "Feature.AddFeature.ExistingFeature",
            "The feature is already existing.");
    }

    public static class MealType
    {
        public static readonly Error ExistingMealType = new Error(
            "MealType.AddMealType.ExistingMealType",
            "The meal type is already existing.");

        public static readonly Error NotExistMealType = new Error(
            "MealType.GetMealType.NotExistMealType",
            "The meal type is not existing.");
    }

    public static class RestaurantBooking
    {
        public static readonly Error BookedTableAtThisTime = new Error(
            "RestaurantBooking.AddRestaurantBooking.BookedTableAtThisTime",
            "This table is already booked at this time.");

        public static readonly Error ShortBookingTime = new Error(
            "RestaurantBooking.AddRestaurantBooking.ShortBookingTime",
            "You have to book the table for more than 15 minutes.");

        public static readonly Error LongBookingTime = new Error(
            "RestaurantBooking.AddRestaurantBooking.LongBookingTime",
            "You have to book the table for less than 60 minutes.");
    }

    public static class RestaurantReview
    {
        public static readonly Error EmptyDescription = new Error(
            "RestaurantReview.AddReview.EmptyDescription",
            "The review description can't be empty.");

        public static readonly Error RatingLessThanOne= new Error(
            "RestaurantReview.AddReview.RatingLessThanOne",
            "The review rating can't be less than one.");

        public static readonly Error RatingGreaterThanFive = new Error(
            "RestaurantReview.AddReview.RatingGreaterThanFive",
            "The review rating can't be greater than five.");
    }

    public static class Cuisine
    {
        public static readonly Error NotExistCuisine = new Error(
            "Cuisine.GetCuisine.NotExistCuisine",
            "This cuisine does not exist in the system.");
    }

    public static class CurrencyType
    {
        public static readonly Error NotExistCurrencyType = new Error(
            "Cuisine.GetCurrencyType.NotExistCurrencyType",
            "This currency type does not exist in the system.");
    }
}
