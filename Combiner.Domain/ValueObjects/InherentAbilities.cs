namespace Combiner.Domain.ValueObjects
{
    public class InherentAbilities : ValueObject<InherentAbilities>
    {
        public InherentAbilities(
            bool hasHerding,
            bool hasImmunity,
            bool hasLoner,
            bool hasOverpopulation,
            bool hasPackHunter,
            bool hasRegeneration)
        {
            this.HasHerding = hasHerding;
            this.HasImmunity = hasImmunity;
            this.HasLoner = hasLoner;
            this.HasOverpopulation = hasOverpopulation;
            this.HasPackHunter = hasPackHunter;
            this.HasRegeneration = hasRegeneration;
        }

        public bool HasHerding { get; }

        public bool HasImmunity { get; }

        public bool HasLoner { get; }

        public bool HasOverpopulation { get; }

        public bool HasPackHunter { get; }

        public bool HasRegeneration { get; }

        protected override bool EqualsCore(InherentAbilities other)
        {
            throw new System.NotImplementedException();
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
