using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; set; }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public bool Equals(Entity? other)
        {
            if(other is null)
                return false;

            if(other.GetType() != GetType()) 
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if(obj.GetType() != GetType()) 
                return false;

            if(obj is not Entity entity) 
                return false;

            return Id == entity.Id;
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            return left is not null && right is not null && left.Equals(right);
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}