namespace Pingvinius.Framework.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EnumExtension
    {
        public static IEnumerable<T> GetValues<T>()
        {
            foreach (var enumValue in Enum.GetValues(typeof(T)))
            {
                yield return (T)enumValue;
            }
        }
    }
}