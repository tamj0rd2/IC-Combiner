namespace Combiner.UnitTests
{
    using Combiner.Domain.ValueObjects;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class StockTests
    {
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
        [TestCase(TestHelpers.ManOWar, 6)]
        public void HasCorrectBasicDetails(string scriptName, int size)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var stock = new Stock(scriptName, attributes);

            // Assert
            stock.Should().NotBeNull();
            stock.Name.Should().Be(scriptName);
            stock.Size.Should().Be(size);
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
        [TestCase(TestHelpers.ManOWar, 6, true, false, false, true)]
        public void HasCorrectBodyParts(
            string scriptName, int size, bool hasFront, bool hasBack, bool hasWings, bool hasClaws)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

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

        [TestCase("somethingthatdoesntexist")]
        public void HasCorrectInherentAbilities(string scriptName)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var stock = new Stock(scriptName, attributes);

            // Assert
            stock.InherentAbilities.Should().NotBeNull();
        }
    }
}
