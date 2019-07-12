using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public static class StringHelper
    {
        public static int[] ToIntArray(this string stringValue)
        {
            var intArray = Array.ConvertAll(stringValue.ToStringArray(), int.Parse);
            return intArray;
        }

        public static string[] ToStringArray (this string stringValue)
        {
            var stringArray = stringValue.Split();
            return stringArray;
        }
    }
}
