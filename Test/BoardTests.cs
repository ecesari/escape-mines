using System;
using System.Reflection;
using Domain;
using Moq;
using Service;
using Xunit;

namespace Test
{
    public class BoardTests
    {
        [Theory]
        [InlineData("5 4 ")]
        public void GetBoard_StringCommand_ReturnsBoard(string value)
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object,turtleServiceStub.Object);

            boardService.Create(value);
            var board = boardService.GetBoard();
            Assert.NotNull(board);
        }

        [Theory]
        [InlineData("5 4 ", 5)]
        public void GetBoard_StringCommand_ReturnsBoardWidth(string value, int expectedOutput)
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create(value);
            var board = boardService.GetBoard();
            Assert.Equal(board.Width, expectedOutput);
        }

        [Theory]
        [InlineData("5 4 ", 4)]
        public void GetBoard_StringCommand_ReturnsBoardHeight(string value, int expectedOutput)
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create(value);
            var board = boardService.GetBoard();
            Assert.Equal(board.Height, expectedOutput);
        }





        [Fact]
        public void CreateMines_EmptyBoard_ThrowsNullException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            Assert.Throws<NullReferenceException>(() => boardService.CreateMines("0,0"));
        }

        [Fact]
        public void CreateMines_InvalidPosition_ThrowsNullException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create("0 0");
            Assert.Throws<Exception>(() => boardService.CreateMines("1,1"));
        }


        //[Fact]
        //public void CreateMines_MineExistsInLocation_ThrowsNullException()
        //{
        //    var mineServiceStub = new Mock<IMineService>();
        //    var coordinateServiceStub = new Mock<ICoordinateService>();
        //    var turtleServiceStub = new Mock<ITurtleService>();
        //    var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
        //    boardService.Create("0 0");
        //    Assert.Throws<Exception>(() => boardService.CreateMines("1,1 1,1"));
        //}

        [Theory]
        [InlineData("5 4", "1,1 1,3 3,3", new[] { 1, 1 }, new[] { 1, 3 }, new[] { 3, 3 }, 3)]
        public void CreateMines_StringCommandInput_ReturnsMineCount(string boardInput, string stringMineInput, int[] mineInput1, int[] mineInput2, int[] mineInput3, int expectedResult)
        {
            var mineServiceStub = new Mock<IMineService>();
            mineServiceStub.Setup(x => x.CreateMine(mineInput1))
                .Returns(new Mine { Position = new Coordinate { X = mineInput1[0], Y = mineInput1[1] } });
            mineServiceStub.Setup(x => x.CreateMine(mineInput2))
                .Returns(new Mine { Position = new Coordinate { X = mineInput2[0], Y = mineInput2[1] } });
            mineServiceStub.Setup(x => x.CreateMine(mineInput3))
                .Returns(new Mine { Position = new Coordinate { X = mineInput3[0], Y = mineInput3[1] } });
            var coordinateServiceStub = new Mock<ICoordinateService>();

            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);

            boardService.Create(boardInput);
            boardService.CreateMines(stringMineInput);

            var result = boardService.GetBoard().Mines.Count;
            Assert.Equal(expectedResult, result);
        }




        [Fact]
        public void CreateExit_EmptyBoard_ThrowsNullException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            Assert.Throws<NullReferenceException>(() => boardService.CreateExit("0 0"));
        }

        [Fact]
        public void CreateExit_InvalidPosition_ThrowsException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(1, 1))
                .Returns(new Coordinate { X = 1, Y = 1 });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create("0 0");
            Assert.Throws<Exception>(() => boardService.CreateExit("1 1"));
        }


        [Fact]
        public void CreateExit_MineExistsInLocation_ThrowsNullException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(1, 1))
                .Returns(new Coordinate { X = 1, Y = 1 });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);

            boardService.Create("0 0");
            boardService.CreateMines("0,0");
            Assert.Throws<Exception>(() => boardService.CreateExit("1 1"));
        }

        [Fact]
        public void CreateExit_StringCommand_ReturnsExitCoordinate()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(0, 0)).Returns(new Coordinate { X = 0, Y = 0 });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create("0 0");
            boardService.CreateExit("0 0");
            var result = boardService.GetBoard().ExitPoint;
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("4 2 ", 4, 2, "5 5")]
        public void CreateExit_StringCommand_ReturnsExitCoordinateX(string exitCoordinate, int exitCoordinateX, int exitCoordinateY, string boardDimension)
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(exitCoordinateX, exitCoordinateY)).Returns(new Coordinate { X = exitCoordinateX, Y = exitCoordinateY });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create(boardDimension);

            boardService.CreateExit(exitCoordinate);
            var result = boardService.GetBoard().ExitPoint.X;
            Assert.Equal(exitCoordinateX, result);
        }

        [Theory]
        [InlineData("4 2 ", 4, 2, "5 5")]
        public void CreateExit_StringCommand_ReturnsExitCoordinateY(string exitCoordinate, int exitCoordinateX, int exitCoordinateY, string boardDimension)
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(exitCoordinateX, exitCoordinateY)).Returns(new Coordinate { X = exitCoordinateX, Y = exitCoordinateY });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create(boardDimension);

            boardService.CreateExit(exitCoordinate);
            var result = boardService.GetBoard().ExitPoint.Y;
            Assert.Equal(exitCoordinateY, result);
        }




        [Fact]
        public void CreateTurtle_EmptyBoard_ThrowsNullException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            Assert.Throws<NullReferenceException>(() => boardService.CreateTurtle("0 0"));
        }

        [Fact]
        public void CreateTurtle_InvalidPosition_ThrowsException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(1, 1))
                .Returns(new Coordinate { X = 1, Y = 1 });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);
            boardService.Create("0 0");
            Assert.Throws<Exception>(() => boardService.CreateTurtle("1 1 N"));
        }

        [Fact]
        public void CreateTurtle_MineExistsInLocation_ThrowsException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(1, 1))
                .Returns(new Coordinate { X = 1, Y = 1 });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);

            boardService.Create("0 0");
            Assert.Throws<Exception>(() => boardService.CreateTurtle("1 1 N"));
        }

        [Fact]
        public void CreateTurtle_TurtleAlreadyInitialized_ThrowsException()
        {
            var mineServiceStub = new Mock<IMineService>();
            var coordinateServiceStub = new Mock<ICoordinateService>();
            coordinateServiceStub.Setup(x => x.Create(0, 0))
                .Returns(new Coordinate { X = 0, Y = 0 });
            coordinateServiceStub.Setup(x => x.Create(1, 1))
                .Returns(new Coordinate { X = 1, Y = 1 });
            var turtleServiceStub = new Mock<ITurtleService>();
            var boardService = new BoardService(mineServiceStub.Object, coordinateServiceStub.Object, turtleServiceStub.Object);

            boardService.Create("0 0");
            boardService.CreateTurtle("0 0 N");
            Assert.Throws<Exception>(() => boardService.CreateTurtle("1 1 N"));
        }
    }
}
