namespace Combiner.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;

    using MoonSharp.Interpreter;

    public static class TestHelpers
    {
        public const string ManOWar = "siphonophore";

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static IReadOnlyDictionary<string, double> GetStockAttributes(string scriptName)
        {
            var luaPath = $"D:\\coding\\IC-Combiner\\Combiner\\Stock\\2.5\\{scriptName}.lua";
            var script = new Script();
            script.DoFile(luaPath);
            var attributes = script.Globals["limbattributes"] as Table;
            return attributes.Pairs.ToDictionary(pair => pair.Key.CastToString(), pair => pair.Value.Table.Get(2).Number);
        }
    }
}
