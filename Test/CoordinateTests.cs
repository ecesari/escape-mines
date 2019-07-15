using Service;
using Xunit;

namespace Test
{
    public class CoordinateTests
    {
        [Fact]
        public void CreateCoordinate_InputCoordinates_ReturnsCoordinateNotNull()
        {
            var coordinateService = new CoordinateService();
            var coordinate = coordinateService.Create(0, 0);
            var result = coordinate.X;
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(3, 3, 3)]
        public void CreateCoordinate_InputCoordinates_ReturnsCoordinateX(int x, int y, int expectedValue)
        {

            var coordinateService = new CoordinateService();
            var coordinate = coordinateService.Create(x, y);
            var result = coordinate.X;
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(3, 3, 3)]
        public void CreateCoordinate_InputCoordinates_ReturnsCoordinateY(int x, int y, int expectedValue)
        {

            var coordinateService = new CoordinateService();
            var coordinate = coordinateService.Create(x, y);
            var result = coordinate.X;
            Assert.Equal(expectedValue, result);
        }
    }
}
