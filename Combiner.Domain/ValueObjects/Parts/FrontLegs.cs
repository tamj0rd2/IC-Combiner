namespace Combiner.Domain.ValueObjects.Parts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Combiner.Domain.Resources;
    using Combiner.Domain.ValueObjects.Attacks;

    public sealed class FrontLegs : ValueObject<FrontLegs>
    {
        public FrontLegs(IReadOnlyDictionary<string, double> attributes)
        {
            this.HitPoints = attributes[LuaResources.HitpointsFront];
            this.Armour = attributes[LuaResources.ArmourFront];
            this.LandSpeed = attributes[LuaResources.SpeedMaxFront];
            this.HasDigging = (int)attributes[LuaResources.CanDig] == 1;

            if (attributes.ContainsKey(LuaResources.Melee2DamageType))
            {
                var meleeDamageType = attributes[LuaResources.Melee2DamageType];
                this.MeleeAttack = new MeleeAttack((MeleeAttack.MeleeDamageType)meleeDamageType);
            }

            if (attributes.ContainsKey(LuaResources.Range2DamageType))
            {
                this.RangeAttack = new RangeAttack(
                    (RangeAttack.RangeDamageType)attributes[LuaResources.Range2DamageType],
                    attributes[LuaResources.Range2Damage],
                    attributes[LuaResources.Range2Max],
                    (RangeAttack.RangeSpecial)attributes[LuaResources.Range2Special]);
            }
        }

        public double HitPoints { get; }

        public double Armour { get; }

        public double LandSpeed { get; }

        [Required]
        public bool HasDigging { get; }

        public MeleeAttack MeleeAttack { get; }

        public RangeAttack RangeAttack { get; }

        protected override bool EqualsCore(FrontLegs other)
        {
            return this.HitPoints == other.HitPoints
                   && this.Armour == other.Armour
                   && this.LandSpeed == other.LandSpeed
                   && this.HasDigging == other.HasDigging
                   && this.MeleeAttack == other.MeleeAttack
                   && this.RangeAttack == other.RangeAttack;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = this.HitPoints;
                hashCode = Math.Pow(hashCode * 397, this.Armour);
                hashCode = Math.Pow(hashCode * 397, this.LandSpeed);
                hashCode = Math.Pow(hashCode * 397, this.HasDigging.GetHashCode());
                hashCode = Math.Pow(hashCode * 397, this.MeleeAttack?.GetHashCode() ?? 0);
                hashCode = Math.Pow(hashCode * 397, this.RangeAttack?.GetHashCode() ?? 0);
                return hashCode.GetHashCode();
            }
        }
    }
}
