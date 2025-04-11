using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Domain.Errors
{
    public class DomainErrors
    {
        public static class Email
        {
            public static readonly Error Empty = new("Email.Empty", "Email is empty.");

            public static readonly Error InvalidFormat = new("Email.InvalidFormat", "The email format is invalid.");
        }

        public static class FirstName
        {
            public static readonly Error Empty = new("FirstName.Empty","First name is empty.");

            public static readonly Error TooLong = new("FirstName.TooLong","First name is too long");
        }

        public static class LastName
        {
            public static readonly Error Empty = new("LastName.Empty", "Last name is empty.");

            public static readonly Error TooLong = new("LastName.TooLong", "Last name is too long");
        }

        public static class Password
        {
            public static readonly Error Empty = new("Password.Empty", "Password is empty.");

            public static readonly Error TooShort = new("Password.TooShort","Password is too short.");
        }
    }
}
