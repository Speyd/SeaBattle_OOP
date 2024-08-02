using Field;
using System.Numerics;
using TypeBoat;

namespace AdderBoat
{
    public class Adder(MainField mainField)
    {
        private bool checkerFreePlace(Boat boat)
        {
            int emptyCellCounter = 0;
            foreach(PartBoat partBoat in boat.getPartBoats())
            {
                if (partBoat.Position.Y < 0 || partBoat.Position.Y >= mainField.fieldInfo.Line ||
                    partBoat.Position.X < 0 || partBoat.Position.X >= mainField.fieldInfo.Column)
                {
                    return false;
                }
                else if (mainField.field[(int)partBoat.Position.Y][(int)partBoat.Position.X] == mainField.fieldInfo.EmptyCell)
                    emptyCellCounter++;
            }

            return emptyCellCounter == boat.Size ? true : false;
        }

        public void addBoat(Boat boat)
        {
            if (checkerFreePlace(boat) == false)
            {
                Console.WriteLine("Add Error!");
                return;
            }

            mainField.boats.Add(boat);
            mainField.updateFieldWithBoats();
        }
        public void addBoat(Boat[] boats)
        {
            int amountFreePlace = 0;
            foreach(Boat boat in boats)
            {
                if (checkerFreePlace(boat) == true)
                    amountFreePlace++;
            }

            if (amountFreePlace != boats.Length)
            {
                Console.WriteLine("Add Error!");
                return;
            }

            mainField.boats.AddRange(boats);
            mainField.updateFieldWithBoats();
        }

    }
}
