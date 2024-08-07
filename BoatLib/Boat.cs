using Coordinates2D;
using System;
using System.Numerics;

namespace BoatLib
{
    public class Boat
    {
        public int Size { get; init; }
        public ShipCondition Condition { get; set; } = ShipCondition.WHOLE;
        public DirectionAddition DirectionAdd { get; set; }

        private PartBoat[] boat = { };


        public static RefDelegate directionDetermination(DirectionAddition directionAdd)
        {
            switch (directionAdd)
            {
                case DirectionAddition.UP:
                    return (ref Coordinates coordinates) => coordinates.Line--;
                case DirectionAddition.DOWN:
                    return (ref Coordinates coordinates) => coordinates.Line++;
                case DirectionAddition.LEFT:
                    return (ref Coordinates coordinates) => coordinates.Column--;
                case DirectionAddition.RIGHT:
                    return (ref Coordinates coordinates) => coordinates.Column++;
            }

            return (ref Coordinates coordinates) => { };
        }
        public void fillPartBoat(Coordinates coordinates, DirectionAddition directionAdd, char symbol)
        {
            RefDelegate action = directionDetermination(directionAdd);

            for (int i = 0; i < Size; i++)
            {
                boat[i] = new PartBoat(coordinates, symbol);
                action(ref coordinates);
            }
        }


        public Boat(int size, Coordinates coordinates, DirectionAddition directionAdd, char symbol)
        {
            Size = size <= 0 ? 1 : size;
            DirectionAdd = directionAdd;

            boat = new PartBoat[Size];
            fillPartBoat(coordinates, directionAdd, symbol);
        }


        public bool findCoordinates(int line, int column)
        {
            for (int i = 0; i < Size; ++i)
            {
                if (boat[i].Position == new Coordinates(line, column))
                    return true;
            }

            return false;
        }
        private PartBoat? findPartCoordinates(Coordinates coordinates)
        {
            for (int i = 0; i < Size; ++i)
            {
                if (boat[i].Position == coordinates)
                    return boat[i];
            }

            return null;
        }


        #region Giver
        public char? getSymbol(int line, int column)
        {
            return findPartCoordinates(new Coordinates(line, column))?.Symbol;
        }
        public char? getSymbol(int index)
        {
            if (index < 0 || index >= Size)
                throw new IndexOutOfRangeException("index");
            return boat[index].Symbol;
        }
        public char? getSymbol(Coordinates coordinates)
        {
            return findPartCoordinates(coordinates)?.Symbol;
        }


        public Coordinates[] getCoordinates()
        {
            Coordinates[] coordinates = new Coordinates[Size];
            for(int i = 0; i < Size; ++i)
                coordinates[i] = boat[i].Position;

            return coordinates;
        }
        public Coordinates getCoordinate(int index)
        {
            if(index < 0 || index >= Size) 
                throw new ArgumentOutOfRangeException("index");

            return boat[index].Position;
        }


        public PartBoat getPartBoat(int index)
        {
            if( index < 0 || index >= Size)
                throw new ArgumentOutOfRangeException("index");

            return boat[index];
        }
        public PartBoat[] getPartBoats() => boat;
        #endregion
    }

    public delegate void RefDelegate(ref Coordinates coordinate);
}
