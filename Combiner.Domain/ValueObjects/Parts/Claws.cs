namespace Combiner.Domain.ValueObjects.Parts
{
    using System.Collections.Generic;

    using Combiner.Domain.Resources;
    using Combiner.Domain.ValueObjects.Attacks;

    public class Claws : ValueObject<Claws>
    {
        public Claws(IReadOnlyDictionary<string, double> attributes)
        {
            if (attributes.ContainsKey(LuaResources.Melee8DamageType))
            {
                var damageType = (MeleeAttack.MeleeDamageType)attributes[LuaResources.Melee8DamageType];
                this.MeleeAttack = new MeleeAttack(damageType);
            }

            if (attributes.ContainsKey(LuaResources.Range8DamageType))
            {
                this.RangeAttack = new RangeAttack(
                    (RangeAttack.RangeDamageType)attributes[LuaResources.Range8DamageType],
                    attributes[LuaResources.Range8Damage],
                    attributes[LuaResources.Range8Max],
                    (RangeAttack.RangeSpecial)attributes[LuaResources.Range8Special]);
            }

            if (attributes.ContainsKey(LuaResources.PoisonPincers))
            {
                this.HasPoisonPincers = (int)attributes[LuaResources.PoisonPincers] == 1;
            }
        }

        public MeleeAttack MeleeAttack { get; }

        public RangeAttack RangeAttack { get; }

        public bool HasPoisonPincers { get; }

        protected override bool EqualsCore(Claws other)
        {
            return this.MeleeAttack == other.MeleeAttack &&
                   this.RangeAttack == other.RangeAttack &&
                   this.HasPoisonPincers == other.HasPoisonPincers;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                double hashCode = this.MeleeAttack.GetHashCode();
                hashCode = this.GetHashResult(hashCode, this.RangeAttack);
                hashCode = this.GetHashResult(hashCode, this.HasPoisonPincers);
                return hashCode.GetHashCode();
            }
        }
    }
}
