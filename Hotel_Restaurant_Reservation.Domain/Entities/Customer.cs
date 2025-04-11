using Hotel_Restaurant_Reservation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Domain.Entities
{
    public  class Customer
    {
        public Guid Id { get; set; }

        public FirstName FirstName { get; set; }

        public LastName LastName { get; set; }

        public Email Email { get; set; }

        public Password Password { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Preferences { get; set; }
    }
}
