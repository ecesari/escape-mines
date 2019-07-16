using System.Text.RegularExpressions;

namespace Helper.Helpers
{
    public static class RegexHelper
    {
        public static bool IsSpaceDelimitedNumbers(this string stringValue)
        {
            var regex = new Regex(@"^\d+(?:\s+\d+){1}$");
            var match = regex.Match(stringValue);
            return match.Success;
        }

        public static bool IsSpaceDelimitedArrays(this string stringValue)
        {
            stringValue = stringValue.Replace(" ", ",");
            var regex = new Regex(@"^[0-9](,[0-9])+$");
            var match = regex.Match(stringValue);
            return match.Success;
        }

        public static bool IsSpaceDelimitedLetters(this string stringValue,string letters)
        {
            var pattern = $"^[{letters}]{{1}}( [{letters}]){{0,100}}$";
            var regex = new Regex(pattern);
            var match = regex.Match(stringValue);
            return match.Success;
        }

        public static bool IsSpaceDelimitedNumbersAndChar(this string stringValue)
        {
            stringValue = stringValue.Replace(" ", "");
            var regex = new Regex(@"[0-9]{2}[NSWE]{1}");
            var match = regex.Match(stringValue);
            return match.Success;
        }
    }
}