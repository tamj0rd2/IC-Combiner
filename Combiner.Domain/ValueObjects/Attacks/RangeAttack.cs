namespace Combiner.Domain.ValueObjects.Attacks
{
    using System;

    public class RangeAttack : ValueObject<RangeAttack>
    {
        public enum RangeSpecial
        {
            Unknown = -1,
            Normal = 0,
            Rock = 1,
            Water = 2,
            Chemical = 3
        }

        public enum RangeDamageType
        {
            Unknown = -1,
            Normal = 0,
            ChemicalArtillery = 1,
            QuillThrow = 2,
            Electric = 8,
            Sonic = 16,
            Venom = 256
        }

        public RangeAttack(RangeDamageType damageType, double damage, double max, RangeSpecial special)
        {
            if (damageType == RangeDamageType.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(damageType));
            }

            if (special == RangeSpecial.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(special));
            }

            this.Damage = damage;
            this.Max = max;
            this.Special = special;
            this.DamageType = damageType;
        }

        public RangeDamageType DamageType { get; }

        public double Damage { get; }

        // TODO: wtf is this even for?
        public double Max { get; }

        public RangeSpecial Special { get; }

        protected override bool EqualsCore(RangeAttack other)
        {
            return this.DamageType == other.DamageType &&
                   this.Max == other.Max &&
                   this.Special == other.Special &&
                   this.DamageType == other.DamageType;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                double hashCode = (int)this.DamageType;
                hashCode = this.GetHashResult(hashCode, this.Damage);
                hashCode = this.GetHashResult(hashCode, this.Max);
                hashCode = this.GetHashResult(hashCode, this.Special);
                return hashCode.GetHashCode();
            }
        }
    }
}
