using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Domain.Errors;

public static class DomainErrors
{
    public static class Customer
    {
        public static readonly Error SignUpExistingAccount = new Error("Customer.SignUp.ExistingAccount",
            "Your are trying to signup to an existing account.");
        
        public static readonly Error LogInUnExistingAccount = new Error("Customer.LogIn.UnExistingAccount",
            "Your are trying to login to an unexisting account.");

        public static readonly Error IncorrectPassword = new Error("Customer.LogIn.IncorrectPassword",
            "Your password is incorrect.");
    }

    public static class Dish
    {
        public static readonly Error ExistingDish = new Error("Dish.AddDish.ExistingDish",
            "The dish is already existing.");
    }

    public static class Feature
    {
        public static readonly Error ExistingFeature= new Error("Feature.AddFeature.ExistingFeature",
            "The feature is already existing.");
    }
}
