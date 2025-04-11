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
    public class FirstName : ValueObject
    {
        public string Value { get; }


        private FirstName(string firstName) 
        {
            Value = firstName;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static ResultT<FirstName> Create(string firstName)
        {
            if(string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<FirstName>(DomainErrors.FirstName.Empty);
            }

            return new FirstName(firstName);
        }
    }
}
