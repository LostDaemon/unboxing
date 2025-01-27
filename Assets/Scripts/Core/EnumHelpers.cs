using System;
namespace Core
{
    public static class EnumHelpers
    {
        private const string UndefinedValueName = "Undefined";

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            var random = new Random();
            var values = Enum.GetValues(typeof(T));
            var span = new Span<T>((T[])values);
            var startIndex = span[0].ToString() == UndefinedValueName ? 1 : 0;
            var randomIndex = random.Next(startIndex, span.Length);
            return span[randomIndex];
        }
    }
}