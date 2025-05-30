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
}
