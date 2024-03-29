﻿using System;
using Domain;
using Helper.Enums;
using Helper.Helpers;

namespace Service
{
    public interface ITurtleService
    {
        void Move(string command);
        string GetStatus();
        void CreateTurtle(string command);
        Turtle Create(Coordinate turtleStartingCoordinate, Orientation turtleOrientation);
    }

    public class TurtleService : ITurtleService
    {
        private readonly IBoardService _boardService;
        private readonly ICoordinateService _coordinateService;
        private Turtle _turtle;

        public TurtleService(IBoardService boardService, ICoordinateService coordinateService)
        {
            _boardService = boardService;
            _coordinateService = coordinateService;
        }

        public Turtle Create(Coordinate turtleStartingPosition, Orientation turtleOrientation)
        {
            return _turtle ?? (_turtle = new Turtle
            {
                Position = turtleStartingPosition,
                Orientation = turtleOrientation,
                Status = TurtleStatus.InDanger
            });
        }
        public void CreateTurtle(string command)
        {
            var validCommand = command.IsSpaceDelimitedNumbersAndChar();
            if (!validCommand)
                throw new FormatException("Exit Input Is Not Valid.");


            var array = command.ToStringArray(' ');
            var turtleStartingCoordinate = _coordinateService.Create(Convert.ToInt32(array[0]), Convert.ToInt32(array[1]));

            var exception = _boardService.ValidPosition(turtleStartingCoordinate, "turtle");
            if (exception != null)
                throw exception;

            var turtleOrientation = EnumHelper<Orientation>.GetValueFromName(array[2]);
            var turtle = Create(turtleStartingCoordinate, turtleOrientation);

            var board = _boardService.GetBoard();

            if (board == null)
                throw new NullReferenceException("The board has not been initialized!");
            if (turtle.Board != null)
                throw new Exception("The turtle already has a board!");

            turtle.Board = board;

        }
        public void Move(string command)
        {
            var validCommand = command.IsSpaceDelimitedLetters("LMR");
            if (!validCommand)
                throw new FormatException("Turtle Input Is Not Valid.");

            var array = command.ToStringArray(' ');
            foreach (var move in array)
            {
                if (_turtle.Status != TurtleStatus.InDanger) continue; //Turtle Is Either Dead or Freed, continue
                var movement = EnumHelper<Movement>.GetValueFromName(move);
                switch (movement)
                {
                    case Movement.Left:
                        TurnLeft();
                        break;
                    case Movement.Right:
                        TurnRight();
                        break;
                    case Movement.Move:
                        MoveForward();
                        UpdateStatus();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
                }
            }
        }
        public string GetStatus()
        {
            return EnumHelper<TurtleStatus>.GetDisplayValue(_turtle.Status);
        }

        private void MoveForward()
        {
            switch (_turtle.Orientation)
            {
                case Orientation.North:
                    _turtle.Position.Y++;
                    break;
                case Orientation.West:
                    _turtle.Position.X--;
                    break;
                case Orientation.South:
                    _turtle.Position.Y--;
                    break;
                case Orientation.East:
                    _turtle.Position.X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void UpdateStatus()
        {
            //Check if a mine has been hit
            var mineHit = _boardService.MineExistsInLocation(_turtle.Position.X, _turtle.Position.Y);
            if (mineHit)
            {
                //Update Turtle Status
                _turtle.Status = TurtleStatus.Dead;
                //Update Mine Status
                _boardService.DetonateMineAtLocation(_turtle.Position.X, _turtle.Position.Y);
                return;
            }
            //Check if the turtle has found the exit
            var freed = _boardService.ExitExistsInLocation(_turtle.Position.X, _turtle.Position.Y);
            if (freed)
            {
                _turtle.Status = TurtleStatus.Freed;
                return;
            }
            //Check If Fallen Off
            var newPositionInRange = _boardService.PositionInRange(_turtle.Position.X, _turtle.Position.Y) || _turtle.Position.X < 0 || _turtle.Position.Y < 0;
            if (!newPositionInRange)
                throw new InvalidOperationException("Turtle has fallen off the board. Please re-check your movement input.");
        }
        private void TurnRight()
        {
            _turtle.Orientation = _turtle.Orientation == Orientation.North ? Orientation.East : (Orientation)((int)_turtle.Orientation - 1);

        }
        private void TurnLeft()
        {
            _turtle.Orientation = _turtle.Orientation == Orientation.East ? Orientation.North : (Orientation)((int)_turtle.Orientation + 1);
        }
    }
}