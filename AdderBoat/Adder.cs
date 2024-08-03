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
                if (partBoat.Position.Line < 0 || partBoat.Position.Line >= mainField.fieldInfo.Line ||
                    partBoat.Position.Column < 0 || partBoat.Position.Column >= mainField.fieldInfo.Column)
                {
                    return false;
                }
                else if (mainField.field[partBoat.Position.Line][partBoat.Position.Column] == mainField.fieldInfo.EmptyCell)
                    emptyCellCounter++;
            }

            return emptyCellCounter == boat.Size ? true : false;
        }

        public bool addBoat(Boat boat)
        {
            if (checkerFreePlace(boat) == false)
            {
                Console.WriteLine("Add Error!");
                return false;
            }

            mainField.boats.Add(boat);
            mainField.updateFieldWithBoats();
            return true;
        }
        public bool addBoat(Boat[] boats)
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
                return false;
            }

            mainField.boats.AddRange(boats);
            mainField.updateFieldWithBoats();
            return true;
        }

    }
}
