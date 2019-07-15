using Domain;
using Moq;
using Service;
using Xunit;

namespace Test
{
    public class MineTests
    {
        [Theory]
        [InlineData(new[] { 1, 1 })]
        [InlineData(new[] { 1, 3 })]
        [InlineData(new[] { 3, 3 })]
        public void CreateMine_IntArray_ReturnsMine(int[] mineInput)
        {
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(mineInput[0], mineInput[1]))
                .Returns(new Coordinate { X = mineInput[0], Y = mineInput[1] });
            var mineService = new MineService(coordinateServiceStub.Object);
            var mine = mineService.CreateMine(mineInput);
            Assert.NotNull(mine);
        }

        [Theory]
        [InlineData(new[] { 1, 1 }, 1)]
        [InlineData(new[] { 1, 3 }, 1)]
        [InlineData(new[] { 3, 3 }, 3)]
        public void CreateMine_IntArray_ReturnsValidMinePositionX(int[] mineInput, int expectedValue)
        {
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(mineInput[0], mineInput[1]))
                .Returns(new Coordinate { X = mineInput[0], Y = mineInput[1] });
            var mineService = new MineService(coordinateServiceStub.Object);
            var mine = mineService.CreateMine(mineInput);

            var result = mine.Position.X;
            Assert.Equal(result, expectedValue);
        }

        [Theory]
        [InlineData(new[] { 1, 1 }, 1)]
        [InlineData(new[] { 1, 3 }, 3)]
        [InlineData(new[] { 3, 3 }, 3)]
        public void CreateMine_IntArray_ReturnsValidMinePositionY(int[] mineInput, int expectedValue)
        {
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(mineInput[0], mineInput[1]))
                .Returns(new Coordinate { X = mineInput[0], Y = mineInput[1] });
            var mineService = new MineService(coordinateServiceStub.Object);
            var mine = mineService.CreateMine(mineInput);
            var result = mine.Position.Y;
            Assert.Equal(result, expectedValue);
        }

    }
}