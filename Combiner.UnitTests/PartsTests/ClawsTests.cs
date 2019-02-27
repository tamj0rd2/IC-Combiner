namespace Combiner.UnitTests.PartsTests
{
    using Combiner.Domain.ValueObjects.Attacks;
    using Combiner.Domain.ValueObjects.Parts;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ClawsTests
    {
        [TestCase("lobster", true, false)]
        [TestCase("shrimp", true, true)]
        [TestCase(TestHelpers.ManOWar, true, false)]
        public void HasCorrectClaws(string scriptName, bool hasMelee, bool hasRange)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var claws = new Claws(attributes);

            // Assert
            claws.MeleeAttack.IsNotNull().Should().Be(hasMelee);
            claws.RangeAttack.IsNotNull().Should().Be(hasRange);
        }

        [TestCase("lobster", false)]
        [TestCase("shrimp", false)]
        [TestCase(TestHelpers.ManOWar, true)]
        public void HasCorrectClawsAbilities(string scriptName, bool hasPoisonPincers)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var claws = new Claws(attributes);

            // Assert
            claws.HasPoisonPincers.Should().Be(hasPoisonPincers);
        }

        [TestCase("tarantula", MeleeAttack.MeleeDamageType.Normal)]
        [TestCase(TestHelpers.ManOWar, MeleeAttack.MeleeDamageType.PoisonTip)]
        [TestCase("shrimp", MeleeAttack.MeleeDamageType.BarrierDestroy)]
        [TestCase("lobster", MeleeAttack.MeleeDamageType.BarrierDestroy)]
        public void HasCorrectClawsMelee(string scriptName, MeleeAttack.MeleeDamageType damageType)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var claws = new Claws(attributes);

            // Assert
            claws.MeleeAttack.DamageType.Should().Be(damageType);
        }

        [TestCase("shrimp", RangeAttack.RangeDamageType.Sonic, 2.5, 11.3, RangeAttack.RangeSpecial.Normal)]
        public void HasCorrectClawsRange(
            string scriptName, RangeAttack.RangeDamageType damageType, double damage, double max, RangeAttack.RangeSpecial special)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var claws = new Claws(attributes);

            // Assert
            claws.RangeAttack.DamageType.Should().Be(damageType);
            claws.RangeAttack.Damage.Should().Be(damage);
            claws.RangeAttack.Max.Should().Be(max);
            claws.RangeAttack.Special.Should().Be(special);
        }
    }
}
