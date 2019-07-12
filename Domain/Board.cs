using System;
using System.Collections.Generic;

namespace Domain
{
    public class Board
    {
        //public Coordinate Dimension { get; set; }
        public IList<Mine> Mines { get; set; }
        public Turtle Turtle { get; set; }
        public Coordinate ExitPoint { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
