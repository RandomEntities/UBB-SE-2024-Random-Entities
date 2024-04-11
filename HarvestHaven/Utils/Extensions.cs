namespace HarvestHaven.Utils
{
    public static class Extensions
    {
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }

        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }
    }
}