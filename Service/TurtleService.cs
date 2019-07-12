using System;
using Domain;
using Helper.Enums;
using Helper.Helpers;

namespace Service
{
    public interface ITurtleService
    {
        Turtle Create(Coordinate turtleStartingPosition, Orientation turtleOrientation);
        void Move(Movement movement);
        void Move(Movement movement,Turtle turtle);
        void RunInitial(string command);
        void Move(string command);
        string GetStatus();
    }

    public class TurtleService:ITurtleService
    {
        private readonly ICoordinateService _coordinateService;
        private readonly Turtle _turtle;

        public TurtleService(ICoordinateService coordinateService, Turtle turtle)
        {
            _coordinateService = coordinateService;
            _turtle = turtle;
        }

        public Turtle Create(Coordinate turtleStartingPosition, Orientation turtleOrientation)
        {
            return new Turtle
            {
                Position = turtleStartingPosition,
                Orientation = turtleOrientation
            };
        }


        public void Move(Movement movement,Turtle turtle)
        {
            switch (movement)
            {
                case Movement.Left:
                    turtle.Orientation = turtle.Orientation == Orientation.East ? Orientation.North : (Orientation)((int)turtle.Orientation + 1);
                    break;
                case Movement.Right:
                    turtle.Orientation= turtle.Orientation == Orientation.North ? Orientation.East : (Orientation)((int)turtle.Orientation - 1);
                    break;
                case Movement.Move:
                    switch (turtle.Orientation)
                    {
                        case Orientation.North:
                            turtle.Position.Y++;
                            break;
                        case Orientation.West:
                            turtle.Position.X--;
                            break;
                        case Orientation.South:
                            turtle.Position.Y--;
                            break;
                        case Orientation.East:
                            turtle.Position.X++;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }
        }

        public void RunInitial(string command)
        {
            //add validation
            var turtleStartingCoordinate = _coordinateService.Create(command[0], command[1]);
            var turtleOrientation = EnumHelper<Orientation>.GetValueFromName(command[2].ToString());
            Create(turtleStartingCoordinate, turtleOrientation);
        }

        public void Move(string command)
        {

            foreach (var move in command)
            {
                var movement = EnumHelper<Movement>.GetValueFromName(move.ToString());
                Move(movement, _turtle);
            }
        }

        public string GetStatus()
        {
            return string.Empty;
        }


        public void Move(Movement movement)
        {
            switch (movement)
            {
                case Movement.Left:
                 
                    break;
                case Movement.Right:
                    break;
                case Movement.Move:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }
        }



        private Orientation TurnRight(Orientation orientation)
        {
            return orientation == Orientation.North ? Orientation.East : (Orientation)((int)orientation - 1);

        }

        private Orientation TurnLeft(Orientation orientation)
        {
           return orientation == Orientation.East ? Orientation.North : (Orientation)((int)orientation + 1);

        }

        private void Move(Coordinate position,Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    position.Y++;
                    break;
                case Orientation.West:
                    position.X--;
                    break;
                case Orientation.South:
                    position.Y--;
                    break;
                case Orientation.East:
                    position.X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
