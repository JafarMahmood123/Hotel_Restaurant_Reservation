using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Domain.Errors;

public static class DomainErrors
{
    public static class Customer
    {
        public static readonly Error SignUpExistingAccount = new Error("Customer.SignUp.ExistingAccount",
            "Your are trying to signup to an existing account.");
    }
    public static class A
    {
        public static readonly Error B = new("code", "message");
    }
}
