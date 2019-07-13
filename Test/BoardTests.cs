using System;
using Service;
using Xunit;

namespace Test
{
    public class BoardTests
    {
        private readonly IBoardService _boardService;

        public BoardTests(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [Fact]
        public void ExitShouldBeInBoard()
        {
            //var result = _boardService.ExitCoordinatesAreInBoard();
        }
    }
}
