using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using TypeBoat;

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

        public PartBoat? findBoat(int line, int column)
        {
            foreach(Boat boat in boats)
            {
                foreach (PartBoat partBoat in boat.getPartBoats())
                {
                    if (partBoat.Position == new Vector2(line, column))
                        return partBoat;
                }
            }
            return null;
        }
        public void updateFieldWithBoats()
        {
            foreach (Boat boat in boats)
            {
                foreach (Vector2 coordinates in boat.getCoordinates())
                {
                    field[(int)coordinates.Y][(int)coordinates.X] = boat.getSymbol(coordinates) ?? fieldInfo.EmptyCell;
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
    }
}
