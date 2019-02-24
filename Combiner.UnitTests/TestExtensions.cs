namespace Combiner.UnitTests
{
    using FluentAssertions;

    public static class TestExtensions
    {
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
    }
}
