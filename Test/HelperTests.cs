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
    }
}

