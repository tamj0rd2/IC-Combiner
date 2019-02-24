namespace Combiner.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Combiner.Domain.Resources;
    using Combiner.Domain.ValueObjects.Parts;

    public sealed class BodyParts : ValueObject<BodyParts>
    {
        public BodyParts(IReadOnlyDictionary<string, double> stockAttributes)
        {
            this.Head = new Head();
            this.Torso = new Torso();
            this.Tail = new Tail();

            var hasFrontArmour = stockAttributes[LuaResources.ArmourFront] > 0;
            var hasFrontHitpoints = stockAttributes[LuaResources.HitpointsFront] > 0;
            var hasFrontSpeed = stockAttributes[LuaResources.SpeedMaxFront] > 0;

            if (hasFrontArmour || hasFrontHitpoints || hasFrontSpeed)
            {
                this.FrontLegs = new FrontLegs(stockAttributes);
            }

            if (stockAttributes["speed_max-back"] > 0)
            {
                this.BackLegs = new BackLegs();
            }

            if (stockAttributes["airspeed_max-wings"] > 0)
            {
                this.Wings = new Wings();
            }

            if (stockAttributes.ContainsKey("exp_melee8_damage") || stockAttributes.ContainsKey("exp_range8_damage"))
            {
                this.Claws = new Claws();
            }
        }

        [Required]
        public Head Head { get; }

        [Required]
        public Torso Torso { get; }

        [Required]
        public Tail Tail { get; }

        public FrontLegs FrontLegs { get; }

        public BackLegs BackLegs { get; }

        public Wings Wings { get; }

        public Claws Claws { get; }

        protected override bool EqualsCore(BodyParts other)
        {
            return this.Head == other.Head &&
                   this.Torso == other.Torso &&
                   this.Tail == other.Tail &&
                   this.FrontLegs == other.FrontLegs &&
                   this.BackLegs == other.BackLegs &&
                   this.Wings == other.Wings &&
                   this.Claws == other.Claws;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                double hashCode = this.Head.GetHashCode();
                hashCode = Math.Pow(hashCode * 397, this.Torso.GetHashCode());
                hashCode = Math.Pow(hashCode * 397, this.Tail.GetHashCode());
                hashCode = Math.Pow(hashCode * 397, this.FrontLegs?.GetHashCode() ?? 0);
                hashCode = Math.Pow(hashCode * 397, this.BackLegs?.GetHashCode() ?? 0);
                hashCode = Math.Pow(hashCode * 397, this.Wings?.GetHashCode() ?? 0);
                hashCode = Math.Pow(hashCode * 397, this.Claws?.GetHashCode() ?? 0);
                return hashCode.GetHashCode();
            }
        }
    }
}
