namespace Combiner.Domain.ValueObjects
{
    using System.Collections.Generic;

    using Combiner.Domain.Resources;

    public class InherentAbilities : ValueObject<InherentAbilities>
    {
        public InherentAbilities(IReadOnlyDictionary<string, double> attributes)
        {
            if (attributes.ContainsKey(LuaResources.Ability_Herding))
            {
                this.HasHerding = (int)attributes[LuaResources.Ability_Herding] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Ability_IsImmune))
            {
                this.HasImmunity = (int)attributes[LuaResources.Ability_IsImmune] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Ability_Loner))
            {
                this.HasLoner = (int)attributes[LuaResources.Ability_Loner] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Ability_Overpopulation))
            {
                this.HasOverpopulation = (int)attributes[LuaResources.Ability_Overpopulation] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Ability_PackHunter))
            {
                this.HasPackHunter = (int)attributes[LuaResources.Ability_PackHunter] == 1;
            }

            if (attributes.ContainsKey(LuaResources.Ability_Regeneration))
            {
                this.HasRegeneration = (int)attributes[LuaResources.Ability_Regeneration] == 1;
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
