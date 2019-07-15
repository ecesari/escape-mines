using Domain;
using Helper.Enums;
using Moq;
using Service;
using Xunit;

namespace Test
{
    public class TurtleTests
    {
        [Fact]
        public void Create_ReturnNotNull()
        {
            var coordinateStub = new Mock<ICoordinateService>();
            var boardStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(coordinateStub.Object, boardStub.Object);
            var turtle = turtleService.Create(new Coordinate(), Orientation.East);
            Assert.NotNull(turtle);
        }

        [Fact]
        public void Create_ReturnCoordinateX()
        {
            var coordinateStub = new Mock<ICoordinateService>();
            var boardStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(coordinateStub.Object, boardStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = 0, Y = 0 }, Orientation.East);
            var result = turtle.Position.X;
            Assert.Equal(0, result);
        }

        [Fact]
        public void Create_ReturnCoordinateY()
        {
            var coordinateStub = new Mock<ICoordinateService>();
            var boardStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(coordinateStub.Object, boardStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = 0, Y = 0 }, Orientation.East);
            var result = turtle.Position.Y;
            Assert.Equal(0, result);
        }

        [Fact]
        public void Create_ReturnOrientation()
        {
            var coordinateStub = new Mock<ICoordinateService>();
            var boardStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(coordinateStub.Object, boardStub.Object);
            var turtle = turtleService.Create(new Coordinate(), Orientation.East);
            var result = turtle.Orientation;
            Assert.Equal(Orientation.East, result);
        }
    }
}
