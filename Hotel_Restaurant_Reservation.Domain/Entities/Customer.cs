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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public DateOnly DateOfBirth { get; set; }

        // value object later
        public Email Email { get; set; }

    }
}
