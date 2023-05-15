namespace Pingvinius.Framework.Utilities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The class contains help methods to convert from one object type to another.
    /// </summary>
    public static class ParsingHelper
    {
        /// <summary>
        /// The default values separator used to specify values in one string.
        /// Should be the same with JavaScript constant.
        /// </summary>
        public static readonly string DefaultValuesSeparator = ",";

        /// <summary>
        /// Convert string value to nullable integer value.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <returns>Nullable integer value.</returns>
        public static int? ToNullableInt(string stringValue)
        {
            int temp;
            if (int.TryParse(stringValue, out temp))
            {
                return temp;
            }
            return new Nullable<int>();
        }

        /// <summary>
        /// Splits the specified values in one string to list of integers.
        /// All values which is not integers are skipped.
        /// </summary>
        /// <param name="stringValues">The values in string.</param>
        /// <returns>The list of integers.</returns>
        public static IEnumerable<int> SplitToIntegers(string stringValues)
        {
            List<int> intValues = new List<int>();

            if (!string.IsNullOrWhiteSpace(stringValues))
            {
                string[] splitValues = stringValues.Split(new string[] { ParsingHelper.DefaultValuesSeparator }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var stringValue in splitValues)
                {
                    int? intValue = ParsingHelper.ToNullableInt(stringValue);
                    if (intValue.HasValue)
                    {
                        intValues.Add(intValue.Value);
                    }
                }
            }
            return intValues;
        }
    }
}