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
        private readonly ICoordinateService _coordinateService;
        private readonly IBoardService _boardService;
        private Turtle _turtle;

        public TurtleService(ICoordinateService coordinateService, IBoardService boardService)
        {
            _coordinateService = coordinateService;
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
                while (_turtle.Status == Status.InDanger)
                {
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
            //Check If Fallen Off
        }

        public string GetStatus()
        {
            return EnumHelper<Status>.GetDisplayValue(_turtle.Status);
        }

        public void UpdateStatus()
        {
            var mineHit = _boardService.MineExistsInLocation(_turtle.Position);
            if (mineHit)
            {
                _turtle.Status = Status.Dead;
                return;
            }

            var freed = _boardService.ExitExistsInLocation(_turtle.Position);
            if (freed) _turtle.Status = Status.Freed;
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