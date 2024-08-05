using Coordinates2D;
using Field;
using System.Data.Common;
using System.Numerics;
using TypeBoat;

namespace AttackerBoat
{
    public class Attacker
    {
        public CheckingResult checkingConditions(MainField mainField, int line, int column)
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
                    return CheckingResult.MISS;
                case var cell when cell == mainField.fieldInfo.ShipDefeat:
                    Console.WriteLine("You have already attacked this cell!");
                    return CheckingResult.NO_SUCCESS;
                case var cell when cell == mainField.fieldInfo.MissCell:
                    Console.WriteLine("You have already attacked this cell!");
                    return CheckingResult.NO_SUCCESS;
            }

            return CheckingResult.SUCCESS;
        }
        public CheckingResult attack(MainField mainField, Coordinates coordinates)
        {
            CheckingResult result = checkingConditions(mainField, coordinates.Line, coordinates.Column);         
            if(result != CheckingResult.SUCCESS)
                return result;



            PartBoat? partBoat = mainField.findBoat(coordinates);
            if (partBoat is null)
                return CheckingResult.NO_SUCCESS;

            partBoat.Symbol = mainField.fieldInfo.ShipDefeat;
            mainField.updateFieldWithBoats();
            return CheckingResult.SUCCESS;
        }
    }
}
