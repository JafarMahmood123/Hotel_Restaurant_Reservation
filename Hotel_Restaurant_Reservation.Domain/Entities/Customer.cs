using Hotel_Restaurant_Reservation.Domain.Primitives;
using Hotel_Restaurant_Reservation.Domain.ValueObjects;

namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Customer : Entity
{
    public Customer(Guid id, string firstName, string lastName, string email, string password, DateOnly birthDate
        , string preferences = "") : base(id)
    {
        FirstName = FirstName.Create(firstName).Value;
        LastName =LastName.Create(lastName).Value;
        Email = Email.Create(email).Value;
        Password = Password.Create(password).Value;
        BirthDate = birthDate;
        Preferences = preferences;
    }

    public FirstName FirstName { get; set; }

    public LastName LastName { get; set; }

    public Email Email { get; set; }

    public Password Password { get; set; }

    public DateOnly BirthDate { get; set; }

    public string Preferences { get; set; }
}
