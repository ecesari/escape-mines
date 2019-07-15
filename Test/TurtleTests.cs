using System;
using Domain;
using Helper.Enums;
using Moq;
using Service;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class TurtleTests
    {

        [Fact]
        public void CreateTurtle_ReturnNotNull()
        {
            var boardServiceStub = new Mock<IBoardService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate(), Orientation.East);
            Assert.NotNull(turtle);
        }

        [Fact]
        public void CreateTurtle_ReturnCoordinateX()
        {
            var boardServiceStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = 0, Y = 0 }, Orientation.East);
            var result = turtle.Position.X;
            Assert.Equal(0, result);
        }

        [Fact]
        public void CreateTurtle_ReturnCoordinateY()
        {
            var boardServiceStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = 0, Y = 0 }, Orientation.East);
            var result = turtle.Position.Y;
            Assert.Equal(0, result);
        }

        [Fact]
        public void CreateTurtle_ReturnOrientation()
        {
            var boardServiceStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate(), Orientation.East);
            var result = turtle.Orientation;
            Assert.Equal(Orientation.East, result);
        }


        [Theory]
        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.West, Orientation.South)]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.East, Orientation.North)]
        public void TurnLeft_ReturnUpdatedOrientation(Orientation startingOrientation, Orientation expectedOrientation)
        {
            var boardServiceStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate(), startingOrientation);
            turtleService.Move("L");
            var result = turtle.Orientation;
            Assert.Equal(expectedOrientation, result);
        }


        [Theory]
        [InlineData(Orientation.North, Orientation.East)]
        [InlineData(Orientation.West, Orientation.North)]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.East, Orientation.South)]
        public void TurnRight_ReturnUpdatedOrientation(Orientation startingOrientation, Orientation expectedOrientation)
        {
            var boardServiceStub = new Mock<IBoardService>();
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate(), startingOrientation);
            turtleService.Move("R");
            var result = turtle.Orientation;
            Assert.Equal(expectedOrientation, result);
        }


        [Theory]
        [InlineData(1, 1, Orientation.North, 1)]
        [InlineData(1, 1, Orientation.West, 0)]
        [InlineData(1, 1, Orientation.South, 1)]
        [InlineData(1, 1, Orientation.East, 2)]
        public void Move_MoveForward_ReturnUpdatedPositionX(int x, int y, Orientation direction, int expectedResult)
        {
            var boardServiceStub = new Mock<IBoardService>();
            boardServiceStub.Setup(z => z.PositionInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = x, Y = y }, direction);
            turtleService.Move("M");
            var result = turtle.Position.X;
            Assert.Equal(expectedResult, result);
        }


        [Theory]
        [InlineData(1, 1, Orientation.North)]
        [InlineData(1, 1, Orientation.West)]
        [InlineData(1, 1, Orientation.South)]
        [InlineData(1, 1, Orientation.East)]
        public void Move_UpdateStatus_ReturnMineHitTrue(int x, int y, Orientation direction)
        {
            var boardServiceStub = new Mock<IBoardService>();
            boardServiceStub.Setup(z => z.PositionInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            boardServiceStub.Setup(z => z.MineExistsInLocation(It.IsAny<Coordinate>())).Returns(true);
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = x, Y = y }, direction);
            turtleService.Move("M");
            var result = turtle.Status;
            Assert.Equal(Status.Dead, result);
        }

        [Theory]
        [InlineData(1, 1, Orientation.North)]
        [InlineData(1, 1, Orientation.West)]
        [InlineData(1, 1, Orientation.South)]
        [InlineData(1, 1, Orientation.East)]
        public void Move_UpdateStatus_ReturnFreed(int x, int y, Orientation direction)
        {
            var boardServiceStub = new Mock<IBoardService>();
            boardServiceStub.Setup(z => z.PositionInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            boardServiceStub.Setup(z => z.ExitExistsInLocation(It.IsAny<Coordinate>())).Returns(true);
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = x, Y = y }, direction);
            turtleService.Move("M");
            var result = turtle.Status;
            Assert.Equal(Status.Freed, result);
        }


        [Theory]
        [InlineData(1, 1, Orientation.North)]
        [InlineData(1, 1, Orientation.West)]
        [InlineData(1, 1, Orientation.South)]
        [InlineData(1, 1, Orientation.East)]
        public void Move_UpdateStatus_ReturnStillInDanger(int x, int y, Orientation direction)
        {
            var boardServiceStub = new Mock<IBoardService>();
            boardServiceStub.Setup(z => z.PositionInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var turtleService = new TurtleService(boardServiceStub.Object);
            var turtle = turtleService.Create(new Coordinate { X = x, Y = y }, direction);
            turtleService.Move("M");
            var result = turtle.Status;
            Assert.Equal(Status.InDanger, result);
        }



        [Theory]
        [InlineData(1, 1, Orientation.North)]
        [InlineData(1, 1, Orientation.West)]
        [InlineData(1, 1, Orientation.South)]
        [InlineData(1, 1, Orientation.East)]
        public void Move_UpdateStatus_ReturnInvalidOperationException(int x, int y, Orientation direction)
        {
            var boardServiceStub = new Mock<IBoardService>();
            boardServiceStub.Setup(z => z.PositionInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            var turtleService = new TurtleService(boardServiceStub.Object);
            turtleService.Create(new Coordinate { X = x, Y = y }, direction);
          
            Assert.Throws<InvalidOperationException>(() => turtleService.Move("M"));
        }


        [Theory]
        [InlineData(1, 1, Orientation.North)]
        public void GetStatus_ReturnsStatus(int x, int y, Orientation direction)
        {
            var boardServiceStub = new Mock<IBoardService>();
            boardServiceStub.Setup(z => z.PositionInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var turtleService = new TurtleService(boardServiceStub.Object);
            turtleService.Create(new Coordinate { X = x, Y = y }, direction);
            var result = turtleService.GetStatus();
            Assert.Equal("Still in Danger",result);
        }
    }
}
