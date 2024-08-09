using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using BoatLib;
using Coordinates2D;

namespace Field
{
    public class MainField
    {
        public FieldInfo fieldInfo;

        public List<List<char>> field;
        public List<Boat> boats;

        private void resetField()
        {
            for(int i = 0; i < fieldInfo.Line; i++)
            {
                field.Add(new List<char> { });

                for (int j = 0; j < fieldInfo.Column; j++)
                    field[i].Add(fieldInfo.EmptyCell);
            }
        }

        public MainField(int line, int column, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X') 
        {
            fieldInfo = new FieldInfo(line, column, emptyCell, missCell, shipDefeat);

            boats = new List<Boat>();

            field = new List<List<char>>();
            resetField();
        }

        public void checkShipIntegrity(Coordinates coordinates)
        {
            Boat? boat = findBoat(coordinates);

            if (boat is null || boat.Condition == ShipCondition.DESTROYED)
                return;


            int amountDefeatPart = 0;

            foreach (PartBoat partBoat in boat.getPartBoats())
            {
                if (partBoat.Symbol == fieldInfo.ShipDefeat)
                    amountDefeatPart++;
            }

            if (amountDefeatPart == boat.Size)
                boat.Condition = ShipCondition.DESTROYED;
        }
        public PartBoat? findPartBoat(Coordinates coordinates)
        {
            foreach(Boat boat in boats)
            {
                foreach (PartBoat partBoat in boat.getPartBoats())
                {
                    if (partBoat.Position == coordinates)
                        return partBoat;
                }
            }
            return null;
        }
        public Boat? findBoat(Coordinates coordinates)
        {
            for (int i = 0; i < boats.Count; i++)
            {
                foreach (PartBoat partBoat in boats[i].getPartBoats())
                {
                    if (partBoat.Position == coordinates)
                        return boats[i];
                }
            }
            return null;
        }

        public void updateFieldWithBoats()
        {
            foreach (Boat boat in boats)
            {
                foreach (Coordinates coordinates in boat.getCoordinates())
                {
                    field[coordinates.Line][coordinates.Column] = boat.getSymbol(coordinates) ?? fieldInfo.EmptyCell;
                }
            }
        }
        public void printField()
        {
            for (int i = 0; i < fieldInfo.Line; i++)
            {
                for (int j = 0; j < fieldInfo.Column; j++)
                {
                    Console.Write(field[i][j]);
                }
                Console.WriteLine();
            }
        }

        public void printHiddenField()
        {
            Console.Write(" ");
            for (int i = 0; i < fieldInfo.Column; ++i)
                Console.Write(i);

            Console.WriteLine();

            for (int i = 0; i < fieldInfo.Line; i++)
            {
                Console.Write(i);
                for (int j = 0; j < fieldInfo.Column; j++)
                {
                    if(field[i][j] == fieldInfo.ShipDefeat ||
                       field[i][j] == fieldInfo.MissCell ||
                       field[i][j] == fieldInfo.EmptyCell)
                        Console.Write(field[i][j]);
                    else
                        Console.Write(fieldInfo.EmptyCell);
                }
                Console.WriteLine();
            }
        }
    }
}
