using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Domain.Shared
{
    public class ResultT<TValue> : Result
    {
        private readonly TValue _value;

        protected internal ResultT(bool isSuccess, Error error, TValue value) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator ResultT<TValue>(TValue value) => Create(value);
    }
}
