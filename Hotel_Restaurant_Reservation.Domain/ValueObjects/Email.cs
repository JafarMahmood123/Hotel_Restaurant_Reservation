using Hotel_Restaurant_Reservation.Domain.Errors;
using Hotel_Restaurant_Reservation.Domain.Primitives;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string email) 
        {
            Value = email;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value; 
        }

        public static ResultT<Email> Create(string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>(DomainErrors.Email.Empty);
            }

            if(email.Split('@').Length !=2)
            {
                return Result.Failure<Email>(DomainErrors.Email.InvalidFormat);
            }

            return new Email(email);
        }
    }
}
