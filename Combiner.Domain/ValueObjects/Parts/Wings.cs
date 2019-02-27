namespace Combiner.Domain.ValueObjects.Parts
{
    using System;
    using System.Collections.Generic;

    using Combiner.Domain.Resources;

    public class Wings : ValueObject<Wings>
    {
        public Wings(IReadOnlyDictionary<string, double> attributes)
        {
            this.AirSpeed = (int)attributes[LuaResources.SpeedMax_Airspeed_Wings];
            if (this.AirSpeed < 1)
            {
                throw new ArgumentOutOfRangeException("Expected wings to have airspeed");
            }
        }

        public double AirSpeed { get; }

        protected override bool EqualsCore(Wings other)
        {
            return this.AirSpeed == other.AirSpeed;
        }

        protected override int GetHashCodeCore()
        {
            return this.AirSpeed.GetHashCode();
        }
    }
}
