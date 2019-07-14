using System;
using Helper.Helpers;
using Service;
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
            var result = stringValue.ToTwoDimensionalIntArray(' ',',');
            var array = new[] {new[] {1, 1}, new[] {1, 3}, new[] {3, 3}};
            Assert.Equal(array, result);
        }
    }
}

