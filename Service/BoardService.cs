using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Domain;

namespace Service
{
    public interface IBoardService
    {
        Board Create(Coordinate boardDimensions, List<Mine> mines, Coordinate exit, Turtle turtle);
    }
    public class BoardService:IBoardService
    {
        public Board Create(Coordinate boardDimensions, List<Mine> mines, Coordinate exit,
            Turtle turtle)
        {
            return new Board
            {
                Turtle = turtle,
                Mines = mines,
                Dimension = boardDimensions,
                ExitPoint = exit
            };
        }
    }
}
