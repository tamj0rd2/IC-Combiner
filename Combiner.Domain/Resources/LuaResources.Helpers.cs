namespace Combiner.Domain.Resources
{
    using Combiner.Domain.ValueObjects;

    public partial class LuaResources
    {
        public static string MeleeDamageType(BodyParts.Parts limbId)
        {
            return $"melee{(int)limbId}_dmgtype";
        }

        public static string MeleeDamageScaling(BodyParts.Parts limbId)
        {
            return $"exp_melee{(int)limbId}_damage";
        }

        public static string RangeDamage(BodyParts.Parts limbId)
        {
            return $"range{(int)limbId}_damage";
        }

        public static string RangeDamageType(BodyParts.Parts limbId)
        {
            return $"range{(int)limbId}_dmgtype";
        }

        public static string RangeDamageScaling(BodyParts.Parts limbId)
        {
            return $"exp_range{(int)limbId}_damage";
        }

        public static string RangeMax(BodyParts.Parts limbId)
        {
            return $"range{(int)limbId}_max";
        }

        public static string RangeSpecial(BodyParts.Parts limbId)
        {
            return $"range{(int)limbId}_special";
        }
    }
}
