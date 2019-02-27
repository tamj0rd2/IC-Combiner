namespace Combiner.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;

    using Combiner.Domain.ValueObjects;
    using Combiner.Domain.ValueObjects.Attacks;
    using Combiner.Domain.ValueObjects.Parts;

    using FluentAssertions;

    using MoonSharp.Interpreter;
    using NUnit.Framework;

    [TestFixture]
    public class StockTests
    {
        private Script script;

        private const string ManOWar = "siphonophore";

        [SetUp]
        public void SetUp()
        {
            this.script = new Script();
        }

        [TestCase("albatross", 8)]
        [TestCase("ant", 1)]
        [TestCase("behemoth", 10)]
        [TestCase("blue whale", 12)]
        [TestCase("bull", 5)]
        [TestCase("chameleon", 1)]
        [TestCase("chimpanzee", 2)]
        [TestCase("crocodile", 7)]
        [TestCase("great_white_shark", 7)]
        [TestCase("lobster", 2)]
        [TestCase("shrimp", 1)]
        [TestCase(ManOWar, 6)]
        public void HasCorrectSizeBasicDetails(string scriptName, int size)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var stock = new Stock(scriptName, attributes);

            // Assert
            stock.Should().NotBeNull();
            stock.Name.Should().Be(scriptName);
            stock.Size.Should().Be(size);
            stock.BodyParts.Should().NotBeNull();
            stock.InherentAbilities.Should().NotBeNull();
        }

        [TestCase("albatross", 8, false, true, true, false)]
        [TestCase("ant", 1, true, true, false, false)]
        [TestCase("behemoth", 10, true, true, false, false)]
        [TestCase("blue whale", 12, false, false, false, false)]
        [TestCase("bull", 5, true, true, false, false)]
        [TestCase("chameleon", 1, true, true, false, false)]
        [TestCase("chimpanzee", 2, true, true, false, false)]
        [TestCase("crocodile", 7, true, true, false, false)]
        [TestCase("great_white_shark", 7, false, false, false, false)]
        [TestCase("lobster", 2, true, true, false, true)]
        [TestCase("shrimp", 1, true, true, false, true)]
        [TestCase(ManOWar, 6, true, false, false, true)]
        public void HasCorrectBodyParts(
            string scriptName, int size, bool hasFront, bool hasBack, bool hasWings, bool hasClaws)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var bodyParts = new BodyParts(attributes);

            // Assert
            bodyParts.Should().NotBeNull();
            bodyParts.Head.Should().NotBeNull();
            bodyParts.Torso.Should().NotBeNull();
            bodyParts.Tail.Should().NotBeNull();
            bodyParts.FrontLegs.IsNotNull().Should().Be(hasFront);
            bodyParts.BackLegs.IsNotNull().Should().Be(hasBack);
            bodyParts.Wings.IsNotNull().Should().Be(hasWings);
            bodyParts.Claws.IsNotNull().Should().Be(hasClaws);
        }

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
            var attributes = this.GetStockAttributes(scriptName);

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
            var attributes = this.GetStockAttributes(scriptName);

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
            var attributes = this.GetStockAttributes(scriptName);

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
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var frontLegs = new FrontLegs(attributes);

            // Assert
            frontLegs.RangeAttack.Should().NotBeNull();
            frontLegs.RangeAttack.DamageType.Should().Be(damageType);
            frontLegs.RangeAttack.Damage.Should().Be(damage);
            frontLegs.RangeAttack.Special.Should().Be(special);
        }

        [TestCase("albatross", 16)]
        [TestCase("snowy_owl", 23)]
        public void HasCorrectWings(string scriptName, double expectedSpeed)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var wings = new Wings(attributes);

            // Assert
            wings.AirSpeed.Should().Be(expectedSpeed);
        }

        [TestCase("lobster", true, false)]
        [TestCase("shrimp", true, true)]
        [TestCase(ManOWar, true, false)]
        public void HasCorrectClaws(string scriptName, bool hasMelee, bool hasRange)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var claws = new Claws(attributes);

            // Assert
            claws.MeleeAttack.IsNotNull().Should().Be(hasMelee);
            claws.RangeAttack.IsNotNull().Should().Be(hasRange);
        }

        [TestCase("lobster", false)]
        [TestCase("shrimp", false)]
        [TestCase(ManOWar, true)]
        public void HasCorrectClawsAbilities(string scriptName, bool hasPoisonPincers)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var claws = new Claws(attributes);

            // Assert
            claws.HasPoisonPincers.Should().Be(hasPoisonPincers);
        }

        [TestCase("tarantula", MeleeAttack.MeleeDamageType.Normal)]
        [TestCase(ManOWar, MeleeAttack.MeleeDamageType.PoisonTip)]
        [TestCase("shrimp", MeleeAttack.MeleeDamageType.BarrierDestroy)]
        [TestCase("lobster", MeleeAttack.MeleeDamageType.BarrierDestroy)]
        public void HasCorrectClawsMelee(string scriptName, MeleeAttack.MeleeDamageType damageType)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

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
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var claws = new Claws(attributes);

            // Assert
            claws.RangeAttack.DamageType.Should().Be(damageType);
            claws.RangeAttack.Damage.Should().Be(damage);
            claws.RangeAttack.Max.Should().Be(max);
            claws.RangeAttack.Special.Should().Be(special);
        }

        private IReadOnlyDictionary<string, double> GetStockAttributes(string scriptName)
        {
            var luaPath = $"D:\\coding\\IC-Combiner\\Combiner\\Stock\\2.5\\{scriptName}.lua";
            this.script.DoFile(luaPath);
            var attributes = this.script.Globals["limbattributes"] as Table;
            return attributes.Pairs.ToDictionary(
                pair => pair.Key.CastToString(),
                pair => pair.Value.Table.Get(2).Number);
        }
    }
}
