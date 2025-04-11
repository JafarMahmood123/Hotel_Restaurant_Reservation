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
    public class Password : ValueObject
    {
        public string Value { get; set; }

        private Password(string password)
        {
            Value = password;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static ResultT<Password> Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<Password>(DomainErrors.Password.Empty);
            }

            if (password.Length < 8)
            {
                return Result.Failure<Password>(DomainErrors.Password.TooShort);
            }

            return new Password(password);
        }
    }
}
