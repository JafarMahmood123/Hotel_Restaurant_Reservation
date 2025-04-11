using Hotel_Restaurant_Reservation.Domain.Errors;
using Hotel_Restaurant_Reservation.Domain.Primitives;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Domain.ValueObjects
{
    public class LastName : ValueObject
    {
        public string Value { get; set; }

        private LastName(string lastName)
        {
            Value = lastName;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static ResultT<LastName> Create(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<LastName>(DomainErrors.LastName.Empty);
            }

            if (lastName.Length > 20)
            {
                return Result.Failure<LastName>(DomainErrors.LastName.TooLong);
            }

            return new LastName(lastName);
        }
    }
}
