using System;
using System.Numerics;

namespace TypeBoat
{
    public class Boat
    {
        public int Size { get; init; }
        public DirectionAddition DirectionAdd { get; set; }

        private PartBoat[] boat = { };


        private RefDelegate directionDetermination()
        {
            switch (DirectionAdd)
            {
                case DirectionAddition.UP:
                    return (ref Vector2 coordinates) => coordinates.Y--;
                case DirectionAddition.DOWN:
                    return (ref Vector2 coordinates) => coordinates.Y++;
                case DirectionAddition.LEFT:
                    return (ref Vector2 coordinates) => coordinates.X--;
                case DirectionAddition.RIGHT:
                    return (ref Vector2 coordinates) => coordinates.X++;
            }

            return (ref Vector2 coordinates) => { };
        }
        private void fillPartBoat(Vector2 coordinates, char symbol)
        {
            RefDelegate action = directionDetermination();

            for (int i = 0; i < Size; i++)
            {
                boat[i] = new PartBoat(coordinates, symbol);
                action(ref coordinates);
            }
        }


        public Boat(int size, Vector2 coordinates, DirectionAddition directionAdd, char symbol)
        {
            Size = size <= 0 ? 1 : size;
            DirectionAdd = directionAdd;

            boat = new PartBoat[Size];
            fillPartBoat(coordinates, symbol);
        }


        public bool findCoordinates(int line, int column)
        {
            for (int i = 0; i < Size; ++i)
            {
                if (boat[i].Position == new Vector2(line, column))
                    return true;
            }

            return false;
        }
        private PartBoat? findPartCoordinates(Vector2 coordinates)
        {
            for (int i = 0; i < Size; ++i)
            {
                if (boat[i].Position == coordinates)
                    return boat[i];
            }

            return null;
        }


        public char? getSymbol(int line, int column)
        {
            return findPartCoordinates(new Vector2(line, column))?.Symbol;
        }
        public char? getSymbol(Vector2 coordinates)
        {
            return findPartCoordinates(coordinates)?.Symbol;
        }


        public Vector2[] getCoordinates()
        {
            Vector2[] coordinates = new Vector2[Size];
            for(int i = 0; i < Size; ++i)
                coordinates[i] = boat[i].Position;

            return coordinates;
        }
        public Vector2 getCoordinate(int index)
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
        public PartBoat[] getPartBoats()
        {
            return boat;
        }
    }

    public delegate void RefDelegate(ref Vector2 coordinate);
}
