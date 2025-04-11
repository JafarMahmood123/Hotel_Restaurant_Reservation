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
    }
}
