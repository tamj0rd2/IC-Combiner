namespace Combiner.Domain.ValueObjects.Attacks
{
    using System;

    public class MeleeAttack : ValueObject<MeleeAttack>
    {
        public enum MeleeDamageType
        {
            Unknown = -1,
            Normal = 0,
            PoisonTip = 1,
            Horns = 2,
            BarrierDestroy = 4,
            PoisonBiteSting = 256
        }

        public MeleeAttack(MeleeDamageType damageType)
        {
            if (damageType == MeleeDamageType.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(damageType));
            }

            this.DamageType = damageType;
        }

        public MeleeDamageType DamageType { get; }

        protected override bool EqualsCore(MeleeAttack other)
        {
            return this.DamageType == other.DamageType;
        }

        protected override int GetHashCodeCore()
        {
            return (int)this.DamageType;
        }
    }
}
