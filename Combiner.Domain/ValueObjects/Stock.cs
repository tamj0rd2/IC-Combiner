namespace Combiner.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;

    using Combiner.Domain.Resources;

    public sealed class Stock : ValueObject<Stock>
    {
        public Stock(string name, IReadOnlyDictionary<string, double> stockAttributes)
        {
            this.Name = name;
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new ArgumentOutOfRangeException(nameof(this.Name));
            }

            this.Size = (int)stockAttributes[LuaResources.Size];
            if (this.Size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(this.Name));
            }

            this.BodyParts = new BodyParts(stockAttributes);
            this.InherentAbilities = new InherentAbilities(false, false, false, false, false, false);
        }

        public string Name { get; }

        public int Size { get; }

        public BodyParts BodyParts { get; }

        public InherentAbilities InherentAbilities { get; }

        protected override bool EqualsCore(Stock other)
        {
            return this.Name == other.Name &&
                   this.Size == other.Size &&
                   this.BodyParts == other.BodyParts &&
                   this.InherentAbilities == other.InherentAbilities;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                double hashCode = this.Name?.GetHashCode() ?? 23;
                hashCode = Math.Pow(hashCode * 397, this.Size);
                hashCode = Math.Pow(hashCode * 397, this.BodyParts.GetHashCode());
                hashCode = Math.Pow(hashCode * 397, this.InherentAbilities.GetHashCode());
                return hashCode.GetHashCode();
            }
        }
    }
}
