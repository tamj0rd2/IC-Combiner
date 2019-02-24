namespace Combiner.Domain.ValueObjects
{
    using System;

    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is T valueObject))
            {
                return false;
            }

            return this.EqualsCore(valueObject);
        }

        public override int GetHashCode()
        {
            return this.GetHashCodeCore();
        }

        protected abstract bool EqualsCore(T other);

        protected abstract int GetHashCodeCore();

        protected double GetHashResult(double hashCode, object obj)
        {
            return Math.Pow(hashCode * 397, obj.GetHashCode());
        }
    }
}
