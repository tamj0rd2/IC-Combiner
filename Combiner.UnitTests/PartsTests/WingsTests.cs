namespace Combiner.UnitTests.PartsTests
{
    using Combiner.Domain.ValueObjects.Parts;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class WingsTests
    {
        [TestCase("albatross", 16)]
        [TestCase("snowy_owl", 23)]
        public void HasCorrectWings(string scriptName, double expectedSpeed)
        {
            // Arrange
            var attributes = TestHelpers.GetStockAttributes(scriptName);

            // Act
            var wings = new Wings(attributes);

            // Assert
            wings.AirSpeed.Should().Be(expectedSpeed);
        }
    }
}
