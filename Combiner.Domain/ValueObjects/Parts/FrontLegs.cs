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
            var bodyPart = BodyParts.Parts.Front;

            this.HitPoints = attributes[LuaResources.Hitpoints_Front];
            this.Armour = attributes[LuaResources.Armour_Front];
            this.LandSpeed = attributes[LuaResources.SpeedMax_LandSpeed_Front];
            this.HasDigging = (int)attributes[LuaResources.Ability_CanDig] == 1;

            var meleeDamageTypeKey = LuaResources.MeleeDamageType(bodyPart);
            if (attributes.ContainsKey(meleeDamageTypeKey))
            {
                this.MeleeAttack = new MeleeAttack((MeleeAttack.MeleeDamageType)attributes[meleeDamageTypeKey]);
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
