using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public static class StringHelper
    {
        public static int[] ConvertToIntArray(this string stringValue)
        {
            var intArray = Array.ConvertAll(stringValue.ConvertToStringArray(), int.Parse);
            return intArray;
        }

        public static string[] ConvertToStringArray (this string stringValue)
        {
            var stringArray = stringValue.Split();
            return stringArray;
        }
    }
}
