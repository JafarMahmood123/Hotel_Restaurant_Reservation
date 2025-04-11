using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Domain.Shared
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

        public string Message { get; }

        public string  Code { get; }

        public Error(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public static implicit operator string(Error error) => error.Code;


        public bool Equals(Error? other)
        {
            return other != null && other.Message == Message && other.Code == Code;
        }

        public static bool operator ==(Error? left, Error? right)
        {
            return left is not null && right is not null && left.Equals(right);
        }

        public static bool operator !=(Error? left, Error? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Error error && Equals(error);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
