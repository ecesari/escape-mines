using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Domain;
using Helper;
using Helper.Helpers;


namespace Service
{
    public interface IBoardService
    {

        void RunInitial(string command);
        void AddMines(List<Mine> mineList);
        void AddExit(Coordinate exit);
        void AddTurtle(Turtle turtle);
        bool MineExistsInLocation(Coordinate coordinate);
        bool ExitExistsInLocation(Coordinate coordinate);
    }
    public class BoardService : IBoardService
    {
        private Board _board;

        private void CreateInitial(int width, int height)
        {
            _board = new Board
            {
               Width = width,
               Height = height
            };
        }




        public void RunInitial(string command)
        {
            var boardSizeCoordinates = command.ToIntArray();
            //check if int
            CreateInitial(boardSizeCoordinates[0], boardSizeCoordinates[1]);
        }

        public void AddMines(List<Mine> mineList)
        {
            _board.Mines = mineList;
        }

        public void AddExit(Coordinate exit)
        {
            //check mines
            _board.ExitPoint = exit;
        }

        public void AddTurtle(Turtle turtle)
        {
            _board.Turtle = turtle;
        }

        public bool MineExistsInLocation(Coordinate coordinate)
        {
            return _board.Mines.Any(x => x.Position == coordinate);
        }

        public bool ExitExistsInLocation(Coordinate coordinate)
        {
            return _board.ExitPoint == coordinate;
        }
    }
}
