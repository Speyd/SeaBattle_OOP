using Field;
using TypeBoat;

namespace AttackerBoat
{
    public class Attacker
    {
        private bool checkingConditions(MainField mainField, int line, int column)
        {
            if (line < 0 || line >= mainField.fieldInfo.Line ||
                           column < 0 || column >= mainField.fieldInfo.Column)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            switch (mainField.field[line][column])
            {
                case var cell when cell == mainField.fieldInfo.EmptyCell:
                    mainField.field[line][column] = mainField.fieldInfo.MissCell;
                    Console.WriteLine("Missed!");
                    return false;
                case var cell when cell == mainField.fieldInfo.ShipDefeat:
                    Console.WriteLine("Missed!");
                    return false;
            }

            return true;
        }
        public void attack(MainField mainField, int line, int column)
        {

            if (checkingConditions(mainField, line, column) == false)
                return;



            PartBoat? partBoat = mainField.findBoat(line, column);

            if (partBoat is not null)
                partBoat.Symbol = mainField.fieldInfo.ShipDefeat;
            mainField.updateFieldWithBoats();
        }
    }
}
