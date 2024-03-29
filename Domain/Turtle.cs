﻿using Helper.Enums;

namespace Domain
{
    public class Turtle
    {
        public Coordinate Position { get; set; }
        public Orientation Orientation { get; set; }
        public TurtleStatus Status { get; set; }
        public Board Board { get; set; }
    }
}
