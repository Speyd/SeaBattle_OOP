using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TypeBoat
{
    public class PartBoat
    {
        public Vector2 Position { get; init; }
        public char Symbol { get; set; }

        public PartBoat(Vector2 position, char symbol)
        {
            Position = position;
            Symbol = symbol;
        }
    }
}
