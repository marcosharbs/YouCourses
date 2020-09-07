using System;

namespace Library.Domain.Common
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }

        public Entity(Guid id)
        {
            if(id == null || id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            else {
                Id = id;
            }
        }

        public Entity()
        {
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}