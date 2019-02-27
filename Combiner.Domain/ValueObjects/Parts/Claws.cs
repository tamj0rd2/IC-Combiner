namespace Combiner.Domain.ValueObjects.Parts
{
    using System.Collections.Generic;

    using Combiner.Domain.Resources;
    using Combiner.Domain.ValueObjects.Attacks;

    public class Claws : ValueObject<Claws>
    {
        public Claws(IReadOnlyDictionary<string, double> attributes)
        {
            var bodyPart = BodyParts.Parts.Claws;

            var damageTypeKey = LuaResources.MeleeDamageType(bodyPart);
            if (attributes.ContainsKey(damageTypeKey))
            {
                this.MeleeAttack = new MeleeAttack((MeleeAttack.MeleeDamageType)attributes[damageTypeKey]);
            }

            var rangeDamageTypeKey = LuaResources.RangeDamageType(bodyPart);
            if (attributes.ContainsKey(rangeDamageTypeKey))
            {
                this.RangeAttack = new RangeAttack(
                    (RangeAttack.RangeDamageType)attributes[rangeDamageTypeKey],
                    attributes[LuaResources.RangeDamage(bodyPart)],
                    attributes[LuaResources.RangeMax(bodyPart)],
                    (RangeAttack.RangeSpecial)attributes[LuaResources.RangeSpecial(bodyPart)]);
            }

            if (attributes.ContainsKey(LuaResources.Ability_PoisonPincers))
            {
                this.HasPoisonPincers = (int)attributes[LuaResources.Ability_PoisonPincers] == 1;
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
