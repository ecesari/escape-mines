using System;
using System.Data;
using Domain;
using Helper.Enums;
using Helper.Helpers;

namespace Service
{
    public interface ITurtleService
    {
        void Move(string command);
        string GetStatus();
        Turtle Create(Coordinate turtleStartingCoordinate, Orientation turtleOrientation);
    }

    public class TurtleService : ITurtleService
    {
        private readonly IBoardService _boardService;
        private Turtle _turtle;

        public TurtleService(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public Turtle Create(Coordinate turtleStartingPosition, Orientation turtleOrientation)
        {
            return _turtle ?? (_turtle = new Turtle
            {
                Position = turtleStartingPosition,
                Orientation = turtleOrientation,
                Status = Status.InDanger
            });
        }

        public void Move(string command)
        {
            foreach (var move in command)
            {
                var movement = EnumHelper<Movement>.GetValueFromName(move.ToString());
                if (_turtle.Status != Status.InDanger) continue; //Turtle Is Either Dead or Freed, continue
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

        public string GetStatus()
        {
            return EnumHelper<Status>.GetDisplayValue(_turtle.Status);
        }



        private void UpdateStatus()
        {
            //Check if a mine has been hit
            var mineHit = _boardService.MineExistsInLocation(_turtle.Position);
            if (mineHit)
            {
                _turtle.Status = Status.Dead;
                return;
            }
            //Check if the turtle has found the exit
            var freed = _boardService.ExitExistsInLocation(_turtle.Position);
            if (freed)
            {
                _turtle.Status = Status.Freed;
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