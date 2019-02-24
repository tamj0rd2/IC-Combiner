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
        [TestCase("siphonophore", 6)]
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
        [TestCase("siphonophore", 6, true, false, false, true)]
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
            bodyParts.FrontLegs.IsNull().Should().Be(!hasFront);
            bodyParts.BackLegs.IsNull().Should().Be(!hasBack);
            bodyParts.Wings.IsNull().Should().Be(!hasWings);
            bodyParts.Claws.IsNull().Should().Be(!hasClaws);
        }

        [TestCase("ant", 6, 0.07, 10.4, true, true, false)]
        [TestCase("behemoth", 100, 0.07, 8, false, false, false)]
        [TestCase("bull", 44.2, 0.05, 11, false, false, false)]
        [TestCase("chameleon", 5, 0.04, 6.4, false, false, false)]
        [TestCase("chimpanzee", 13, 0.04, 11.2, false, true, true)]
        [TestCase("lemming", 6.4, 0.03, 9.2, true, false, false)]
        public void HasCorrectFrontLegs(
            string scriptName, double hp, double armour, double landSpeed, bool hasDigging, bool hasMelee, bool hasRange)
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
            frontLegs.HasDigging.Should().Be(hasDigging);
            frontLegs.MeleeAttack.IsNull().Should().Be(!hasMelee);
            frontLegs.RangeAttack.IsNull().Should().Be(!hasRange);
        }

        [TestCase("ant", MeleeAttack.MeleeDamageType.Normal)]
        [TestCase("chimpanzee", MeleeAttack.MeleeDamageType.Normal)]
        public void HasCorrectFrontLegMelee(string scriptName, MeleeAttack.MeleeDamageType damageType)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var frontLegs = new FrontLegs(attributes);

            // Assert
            frontLegs.MeleeAttack.Should().NotBeNull();
            frontLegs.MeleeAttack.DamageType.Should().Be(damageType);
        }

        [TestCase("chimpanzee", RangeAttack.RangeDamageType.Normal)]
        [TestCase("bolas_spider", RangeAttack.RangeDamageType.Normal)]
        public void HasCorrectFrontLegRange(string scriptName, RangeAttack.RangeDamageType damageType)
        {
            // Arrange
            var attributes = this.GetStockAttributes(scriptName);

            // Act
            var frontLegs = new FrontLegs(attributes);

            // Assert
            frontLegs.RangeAttack.Should().NotBeNull();
            frontLegs.RangeAttack.DamageType.Should().Be(damageType);
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
