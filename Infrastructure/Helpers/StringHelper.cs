using System;

namespace Helper.Helpers
{
    public static class StringHelper
    {
        public static int[] ToIntArray(this string stringValue, char separator)
        {
            var stringArray = stringValue.ToStringArray(separator);
            var intArray = Array.ConvertAll(stringArray,int.Parse);
            return intArray;
        }

        public static string[] ToStringArray(this string stringValue,char separator)
        {
            var stringArray = stringValue.Trim().Split(separator);
            return stringArray;
        }

        public static int[][] ToTwoDimensionalIntArray(this string stringValue, char firstSeparator,
            char secondSeparator)
        {
            var firstArray = stringValue.ToStringArray(firstSeparator);
            var array = new int[firstArray.Length][];

            for (var i = 0; i < firstArray.Length; i++)
            {
                var item = firstArray[i];
                var secondArray = item.ToIntArray(secondSeparator);
                array[i] = secondArray;
            }

            return array;
        }
    }
}
