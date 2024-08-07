using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Coordinates2D;

namespace BoatLib
{
    public class PartBoat
    {
        public Coordinates Position { get; init; }
        public char Symbol { get; set; }

        public PartBoat(Coordinates position, char symbol)
        {
            Position = position;
            Symbol = symbol;
        }
    }
}
