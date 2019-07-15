using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Helper.Enums;
using Helper.Helpers;


namespace Service
{
    public interface IBoardService
    {
        void Create(string command);
        void CreateMines(string command);
        void CreateExit(string command);
        bool MineExistsInLocation(int x, int y);
        bool ExitExistsInLocation(int x, int y);
        bool PositionInRange(int x, int y);
        Exception ValidPosition(Coordinate coordinate, string name);
        Board GetBoard();
        void DetonateMineAtLocation(int positionX, int positionY);
    }
    public class BoardService : IBoardService
    {
        private Board _board;
        private readonly IMineService _mineService;
        private readonly ICoordinateService _coordinateService;

        public BoardService(IMineService mineService, ICoordinateService coordinateService)
        {
            _mineService = mineService;
            _coordinateService = coordinateService;
        }

        public void Create(string command)
        {
            var boardSizeCoordinates = command.ToIntArray(' ');
            //Singleton
            if (BoardExists())
                return;

            var width = boardSizeCoordinates[0];
            var height = boardSizeCoordinates[1];

            if (width <= 0 || height <= 0)
                throw new InvalidOperationException("Board Size Values Should Be Greater Than Zero.");

            _board = new Board
            {
                Width = width,
                Height = height,
                Mines = new List<Mine>()
            };
        }
        public void CreateMines(string command)
        {
            var coordinates = command.ToTwoDimensionalIntArray(' ', ',');
            if (!BoardExists())
                throw new NullReferenceException("No board has been found. Please initialize the board before adding mines.");

            foreach (var mineCoordinate in coordinates)
            {
                var x = mineCoordinate[0];
                var y = mineCoordinate[1];

                var mineIsInRange = PositionInRange(x, y);
                if (!mineIsInRange)
                    throw new Exception($"The position of the mine is not in the range of the board!");
                var mineExists = MineExistsInLocation(x, y);
                if (mineExists)
                    throw new Exception($"You cannot put two mines at the same location. Please check your mine coordinates.");

                var mine = _mineService.CreateMine(mineCoordinate);
                _board.Mines.Add(mine);
            }
        }
        public void CreateExit(string command)
        {
            var exit = command.ToIntArray(' ');
            var exitCoordinate = _coordinateService.Create(exit[0], exit[1]);

            var exception = ValidPosition(exitCoordinate, "exit");
            if (exception == null)
            {
                if (_board.ExitPoint != null)
                    throw new Exception("You have already entered a valid exit point!");
                _board.ExitPoint = exitCoordinate;
            }
            else
                throw exception;
        }

        public bool MineExistsInLocation(int x, int y)
        {
            return _board.Mines != null && _board.Mines.Count > 0 && _board.Mines.Any(z => z.Position.X == x && z.Position.Y == y && z.Status == MineStatus.Active);
        }
        public bool ExitExistsInLocation(int x, int y)
        {
            return _board.ExitPoint != null && _board.ExitPoint.X == x && _board.ExitPoint.Y == y;
        }

        public Board GetBoard()
        {
            return _board;
        }

        public void DetonateMineAtLocation(int positionX, int positionY)
        {
            var mine = _board.Mines.FirstOrDefault(x => x.Position.X == positionX && x.Position.Y == positionY);
            _mineService.Detonate(mine);
        }

        public bool PositionInRange(int x, int y)
        {
            return _board.Width >= x && _board.Height >= y;
        }
        public Exception ValidPosition(Coordinate position, string objectName)
        {
            if (!BoardExists())
                return new NullReferenceException("No board has been found. Please initialize the board before adding mines.");
            var positionIsInRange = PositionInRange(position.X, position.Y);
            if (!positionIsInRange)
                return new Exception($"The position of the {objectName} is not in the range of the board!");
            var mineExists = MineExistsInLocation(position.X, position.Y);
            if (mineExists)
                return new Exception($"There is a mine in the location of the {objectName} input!");
            return null;
        }

        private bool BoardExists()
        {
            return _board != null;
        }
    }
}