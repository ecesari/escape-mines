using System;
using System.Collections.Generic;

namespace Domain
{
    public class Field
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public IList<Mine> Mines { get; set; }
        public Turtle Turtle { get; set; }
    }
}
