namespace Combiner.UnitTests.PartsTests
{
    using Combiner.Domain.ValueObjects.Attacks;
    using Combiner.Domain.ValueObjects.Parts;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class FrontLegsTests
    {
        [TestCase("ant", 6, 0.07, 10.4, true, false)]
        [TestCase("behemoth", 100, 0.07, 8, false, false)]
        [TestCase("bull", 44.2, 0.05, 11, false, false)]
        [TestCase("chameleon", 5, 0.04, 6.4, false, false)]
        [TestCase("chimpanzee", 13, 0.04, 11.2, true, true)]
        [TestCase("lemming", 6.4, 0.03, 9.2, false, false)]
        public void HasCorrectFrontLegs(
            string scriptName, double hp, double armour, double landSpeed, bool hasMelee, bool hasRange)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var frontLegs = new FrontLegs(attributes);

            // Assert
            frontLegs.Should().NotBeNull();
            frontLegs.HitPoints.Should().Be(hp);
            frontLegs.Armour.Should().Be(armour);
            frontLegs.LandSpeed.Should().Be(landSpeed);
            frontLegs.MeleeAttack.IsNotNull().Should().Be(hasMelee);
            frontLegs.RangeAttack.IsNotNull().Should().Be(hasRange);
        }

        [TestCase("ant", true)]
        [TestCase("lemming", true)]
        [TestCase("bull", false)]
        [TestCase("elephant", false)]
        public void HasCorrectFrontLegAbilities(string scriptName, bool hasDigging)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var frontLegs = new FrontLegs(attributes);

            // Assert
            frontLegs.HasDigging.Should().Be(hasDigging);
        }

        [TestCase("ant", MeleeAttack.MeleeDamageType.Normal)]
        [TestCase("chimpanzee", MeleeAttack.MeleeDamageType.Normal)]
        public void HasCorrectFrontLegMeleeAttack(string scriptName, MeleeAttack.MeleeDamageType damageType)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var frontLegs = new FrontLegs(attributes);

            // Assert
            frontLegs.MeleeAttack.Should().NotBeNull();
            frontLegs.MeleeAttack.DamageType.Should().Be(damageType);
        }

        [TestCase("chimpanzee", RangeAttack.RangeDamageType.Normal, 3.5, RangeAttack.RangeSpecial.Rock)]
        [TestCase("bolas_spider", RangeAttack.RangeDamageType.Normal, 5, RangeAttack.RangeSpecial.Normal)]
        public void HasCorrectFrontLegRangeAttack(
            string scriptName, RangeAttack.RangeDamageType damageType, double damage, RangeAttack.RangeSpecial special)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var frontLegs = new FrontLegs(attributes);

            // Assert
            frontLegs.RangeAttack.Should().NotBeNull();
            frontLegs.RangeAttack.DamageType.Should().Be(damageType);
            frontLegs.RangeAttack.Damage.Should().Be(damage);
            frontLegs.RangeAttack.Special.Should().Be(special);
        }
    }
}
