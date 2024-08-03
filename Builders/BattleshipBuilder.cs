using Coordinates2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TypeBoat;

namespace Builders
{
    public class BattleshipBuilder : IBuilder
    {

        private static readonly char symbol = 'B';
        private Boat? boat = null;
        public void reset(Coordinates coordinates, DirectionAddition direction)
        {
            boat = new Boat(4, coordinates, direction, symbol);
        }

        public void setCoordinates(Coordinates coordinates)
        {
            boat?.fillPartBoat(coordinates, boat.DirectionAdd, symbol);
        }

        public void setDirectionAddition(DirectionAddition direction)
        {
            boat?.fillPartBoat(boat.getCoordinate(0), direction, symbol);
        }

        public Boat? getResult()
        {
            Boat? tempBoat = boat;
            boat = null;
            return tempBoat;
        }
    }
}
