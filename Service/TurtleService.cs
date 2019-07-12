using System;
using Domain;
using Infrastructure.Enums;

namespace Service
{
    public interface ITurtleService
    {
        Turtle Create(Coordinate turtleStartingPosition, Orientation turtleOrientation);
    }

    public class TurtleService:ITurtleService
    {
        public Turtle Create(Coordinate turtleStartingPosition, Orientation turtleOrientation)
        {
            return new Turtle
            {
                Position = turtleStartingPosition,
                Orientation = turtleOrientation
            };
        }
    }
}
