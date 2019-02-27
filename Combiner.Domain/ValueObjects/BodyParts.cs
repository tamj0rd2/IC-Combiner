namespace Combiner.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;

    using Combiner.Domain.Resources;
    using Combiner.Domain.ValueObjects.Parts;

    public sealed class BodyParts : ValueObject<BodyParts>
    {
        public enum Parts
        {
            Unknown = -1,
            Front = 2,
            Back = 3,
            Head = 4,
            Tail = 5,
            Torso = 6,
            Wings = 7,
            Claws = 8
        }

        public BodyParts(IReadOnlyDictionary<string, double> stockAttributes)
        {
            this.Head = new Head();
            this.Torso = new Torso();
            this.Tail = new Tail();

            var hasFrontArmour = stockAttributes[LuaResources.Armour_Front] > 0;
            var hasFrontHitpoints = stockAttributes[LuaResources.Hitpoints_Front] > 0;
            var hasFrontSpeed = stockAttributes[LuaResources.SpeedMax_LandSpeed_Front] > 0;

            if (hasFrontArmour || hasFrontHitpoints || hasFrontSpeed)
            {
                this.FrontLegs = new FrontLegs(stockAttributes);
            }

            if (stockAttributes[LuaResources.SpeedMax_LandSpeed_Back] > 0)
            {
                this.BackLegs = new BackLegs();
            }

            if (stockAttributes[LuaResources.SpeedMax_Airspeed_Wings] > 0)
            {
                this.Wings = new Wings(stockAttributes);
            }

            var hasClawMelee = stockAttributes.ContainsKey(LuaResources.MeleeDamageScaling(Parts.Claws));
            var hasClawRange = stockAttributes.ContainsKey(LuaResources.RangeDamageScaling(Parts.Claws));

            if (hasClawMelee || hasClawRange)
            {
                this.Claws = new Claws(stockAttributes);
            }
        }

        public Head Head { get; }

        public Torso Torso { get; }

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
