using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        void CreateTurtle(string command);
        bool MineExistsInLocation(Coordinate coordinate);
        bool ExitExistsInLocation(Coordinate coordinate);
        bool PositionInRange(int x, int y);
    }
    public class BoardService : IBoardService
    {
        private Board _board;
        private readonly IMineService _mineService;
        private readonly ICoordinateService _coordinateService;
        private readonly ITurtleService _turtleService;

        public BoardService(IMineService mineService, ICoordinateService coordinateService, ITurtleService turtleService)
        {
            _mineService = mineService;
            _coordinateService = coordinateService;
            _turtleService = turtleService;
        }

        public void Create(string command)
        {
            var boardSizeCoordinates = command.ToIntArray(' ');
            //Singleton
            if (BoardExists())
                return;

            var width = boardSizeCoordinates[0];
            var height = boardSizeCoordinates[1];
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
            //var duplicateExists = coordinates.HasDuplicateValues();
            //if (duplicateExists)
            //    throw new Exception($"You cannot put two mines at the same location. Please check your mine coordinates.");
            if (!BoardExists())
                throw new NullReferenceException("No board has been found. Please initialize the board before adding mines.");

            foreach (var mineCoordinate in coordinates)
            {
                var x = mineCoordinate[0];
                var y = mineCoordinate[1];

                var mineIsInRange = PositionInRange(x, y);
                if (!mineIsInRange)
                    throw new Exception($"The position of the mine is not in the range of the board!");
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

        public void CreateTurtle(string command)
        {
            var array = command.ToStringArray(' ');
            var turtleStartingCoordinate = _coordinateService.Create(Convert.ToInt32(array[0]), Convert.ToInt32(array[1]));
            var exception = ValidPosition(turtleStartingCoordinate, "turtle");
            if (exception != null)
                throw exception;
            var turtleOrientation = EnumHelper<Orientation>.GetValueFromName(array[2]);

            var turtle = _turtleService.Create(turtleStartingCoordinate, turtleOrientation);
            if (_board.Turtle != null)
                throw new Exception("There is already a turtle in the board!");
            _board.Turtle = turtle;

        }



        public bool MineExistsInLocation(Coordinate coordinate)
        {
            return _board.Mines != null && _board.Mines.Count > 0 && _board.Mines.Any(x => x.Position == coordinate);
        }
        public bool ExitExistsInLocation(Coordinate coordinate)
        {
            return _board.ExitPoint == coordinate;
        }
        public Board GetBoard()
        {
            return _board;
        }




        private bool BoardExists()
        {
            return _board != null;
        }

        public bool PositionInRange(int x, int y)
        {
            return _board.Width >= x && _board.Height >= y;
        }

        private Exception ValidPosition(Coordinate position, string objectName)
        {
            if (!BoardExists())
                return new NullReferenceException("No board has been found. Please initialize the board before adding mines.");
            var positionIsInRange = PositionInRange(position.X, position.Y);
            if (!positionIsInRange)
                return new Exception($"The position of the {objectName} is not in the range of the board!");
            var mineExists = MineExistsInLocation(position);
            if (mineExists)
                return new Exception($"There is a mine in the location of the {objectName} input!");
            return null;
        }
    }
}