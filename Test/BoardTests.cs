using System;
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
        public void StringCommand_ReturnsBoard(string value)
        {
            var boardService = new BoardService();
            boardService.Create(value);
            var board = boardService.GetBoard();
            Assert.NotNull(board);
        }

        [Theory]
        [InlineData("5 4 ", 5)]
        public void StringCommand_ReturnsBoardWidth(string value,int expectedOutput)
        {
            var boardService = new BoardService();
            boardService.Create(value);
            var board = boardService.GetBoard();
            Assert.Equal(board.Width,expectedOutput);
        }

        [Theory]
        [InlineData("5 4 ", 4)]
        public void StringCommand_ReturnsBoardHeight(string value, int expectedOutput)
        {
            var boardService = new BoardService();
            boardService.Create(value);
            var board = boardService.GetBoard();
            Assert.Equal(board.Height, expectedOutput);
        }



        [Fact]
        public void ExitShouldBeInBoard()
        {
            //var result = _boardService.ExitCoordinatesAreInBoard();
        }
    }
}
