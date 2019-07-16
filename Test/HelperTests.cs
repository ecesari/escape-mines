using Helper.Enums;
using Helper.Helpers;
using Xunit;

namespace Test
{
    public class HelperTests
    {
        [Theory]
        [InlineData("5 4 ", new[] { "5", "4" })]
        public void StringValue_ReturnsStringArray(string stringValue, string[] array)
        {
            var result = stringValue.ToStringArray(' ');
            Assert.Equal(array, result);
        }

        [Theory]
        [InlineData("5 4 ", new[] { 5, 4 })]
        public void StringValue_ReturnsIntArray(string stringValue, int[] array)
        {
            var result = stringValue.ToIntArray(' ');
            Assert.Equal(array, result);
        }

        [Theory]
        [InlineData("1,1 1,3 3,3")]
        public void StringValue_ReturnsTwoDimensionalArray(string stringValue)
        {
            var result = stringValue.ToTwoDimensionalIntArray(' ', ',');
            var array = new[] { new[] { 1, 1 }, new[] { 1, 3 }, new[] { 3, 3 } };
            Assert.Equal(array, result);
        }

        [Theory]
        [InlineData("N", Orientation.North)]
        [InlineData("W", Orientation.West)]
        [InlineData("E", Orientation.East)]
        [InlineData("S", Orientation.South)]
        public void GetValueFromName_ReturnsOrientationEnumName(string name, Orientation expected)
        {
            var result = EnumHelper<Orientation>.GetValueFromName(name);
            Assert.Equal(expected,result);
        }

        [Theory]
        [InlineData("L", Movement.Left)]
        [InlineData("M", Movement.Move)]
        [InlineData("R", Movement.Right)]
        public void GetValueFromName_ReturnsMovementEnumName(string name, Movement expected)
        {
            var result = EnumHelper<Movement>.GetValueFromName(name);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0 0", true)]
        [InlineData("5 4", true)]
        [InlineData("0 0 N", false)]
        [InlineData("0 0,1", false)]
        public void IsSpaceDelimitedNumbers_StringInput(string input, bool expectedValue)
        {
           var result = input.IsSpaceDelimitedNumbers();
           Assert.Equal(expectedValue,result);
        }

        [Theory]
        [InlineData("1,1 1,3 3,3", true)]
        [InlineData("0 0 N", false)]
        public void IsSpaceDelimitedArrays_StringInput(string input, bool expectedValue)
        {
            var result = input.IsSpaceDelimitedArrays();
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData("R M R M L L", true)]
        [InlineData("R", true)]
        [InlineData("R M R ML L", false)]
        [InlineData("0 0 N", false)]
        [InlineData("0 0,1", false)]
        [InlineData("1,1 1,3 3,3", false)]
        public void IsSpaceDelimitedLetters_StringInput(string input, bool expectedValue)
        {
            var result = input.IsSpaceDelimitedLetters("LMR");
            Assert.Equal(expectedValue, result);
        }


        [Theory]
        [InlineData("1,1 1,3 3,3", false)]
        [InlineData("0 0 N", true)]
        [InlineData("1 1 1 1 1", false)]
        public void IsSpaceDelimitedNumbersAndChar_ReturnTrue(string input, bool expectedValue)
        {
            var result = input.IsSpaceDelimitedNumbersAndChar();
            Assert.Equal(expectedValue, result);
        }
    }
}

