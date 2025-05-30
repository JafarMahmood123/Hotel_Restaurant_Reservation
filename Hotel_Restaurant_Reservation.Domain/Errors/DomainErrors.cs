using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Domain.Errors;

public static class DomainErrors
{
    public static class A
    {
        public static readonly Error B = new("code", "message");
    }
}
