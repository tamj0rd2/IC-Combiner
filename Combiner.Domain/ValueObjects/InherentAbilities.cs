namespace Combiner.Domain.ValueObjects
{
    using System.Collections.Generic;

    using Combiner.Domain.Resources;

    public class InherentAbilities : ValueObject<InherentAbilities>
    {
        public InherentAbilities(IReadOnlyDictionary<string, double> attributes)
        {
            if (attributes.ContainsKey(LuaResources.Herding))
            {
                this.HasHerding = (int)attributes[LuaResources.Herding] == 1;
            }

            if (attributes.ContainsKey(LuaResources.IsImmune))
            {
                this.HasImmunity = (int)attributes[LuaResources.IsImmune] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Loner))
            {
                this.HasLoner = (int)attributes[LuaResources.Loner] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Overpopulation))
            {
                this.HasOverpopulation = (int)attributes[LuaResources.Overpopulation] == 1;
            }

            if (attributes.ContainsKey(LuaResources.PackHunter))
            {
                this.HasPackHunter = (int)attributes[LuaResources.PackHunter] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Regeneration))
            {
                this.HasRegeneration = (int)attributes[LuaResources.Regeneration] == 1;
            }
        }

        public bool HasHerding { get; }

        public bool HasImmunity { get; }

        public bool HasLoner { get; }

        public bool HasOverpopulation { get; }

        public bool HasPackHunter { get; }

        public bool HasRegeneration { get; }

        protected override bool EqualsCore(InherentAbilities other)
        {
            return this.HasHerding == other.HasHerding &&
                   this.HasImmunity == other.HasImmunity &&
                   this.HasLoner == other.HasLoner &&
                   this.HasOverpopulation == other.HasOverpopulation &&
                   this.HasPackHunter == other.HasPackHunter &&
                   this.HasRegeneration == other.HasRegeneration;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                double hashCode = this.HasHerding.GetHashCode();
                hashCode = this.GetHashResult(hashCode, this.HasImmunity);
                hashCode = this.GetHashResult(hashCode, this.HasLoner);
                hashCode = this.GetHashResult(hashCode, this.HasOverpopulation);
                hashCode = this.GetHashResult(hashCode, this.HasPackHunter);
                hashCode = this.GetHashResult(hashCode, this.HasRegeneration);
                return hashCode.GetHashCode();
            }
        }
    }
}
