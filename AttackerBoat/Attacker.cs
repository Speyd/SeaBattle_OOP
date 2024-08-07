using Coordinates2D;
using Field;
using System.Data.Common;
using System.Numerics;
using BoatLib;

namespace AttackerBoat
{
    public class Attacker
    {
        public CheckingResult checkingConditions(MainField mainField, int line, int column)
        {
            if (line < 0 || line >= mainField.fieldInfo.Line ||
                column < 0 || column >= mainField.fieldInfo.Column)
            {
                return CheckingResult.ERROR;
            }

            switch (mainField.field[line][column])
            {
                case var cell when cell == mainField.fieldInfo.EmptyCell:
                    return CheckingResult.MISS;
                case var cell when cell == mainField.fieldInfo.ShipDefeat:
                    return CheckingResult.NO_SUCCESS;
                case var cell when cell == mainField.fieldInfo.MissCell:
                    return CheckingResult.NO_SUCCESS;
            }

            return CheckingResult.SUCCESS;
        }

        #region EventResult
        private void eventSuccessResult(MainField mainField, Coordinates coordinates)
        {
            PartBoat? partBoat = mainField.findPartBoat(coordinates);
            if (partBoat is null)
                throw new Exception("The ship was not found!");

            partBoat.Symbol = mainField.fieldInfo.ShipDefeat;
            mainField.checkShipIntegrity(coordinates);
            mainField.updateFieldWithBoats();

            Console.WriteLine($"An attack was made on a cell({coordinates.ToString()})");
        }
       
        private void eventNoSuccessResult(MainField mainField, Coordinates coordinates)
        {
            Console.WriteLine("You have already attacked this cell!");
        }
        private void eventMiisResult(MainField mainField, Coordinates coordinates)
        {
            mainField.field[coordinates.Line][coordinates.Column] = mainField.fieldInfo.MissCell;
            Console.WriteLine("Missed!");
        }
        #endregion
        private void verificationActions(CheckingResult result, MainField mainField, Coordinates coordinates)
        {
            switch(result)
            {
                case CheckingResult.NO_SUCCESS:
                    eventNoSuccessResult(mainField, coordinates); break;
                case CheckingResult.SUCCESS:
                    eventSuccessResult(mainField, coordinates); break;
                case CheckingResult.MISS:
                    eventMiisResult(mainField, coordinates); break;
            }
        }
        public CheckingResult attack(MainField mainField, Coordinates coordinates)
        {
            CheckingResult result = checkingConditions(mainField, coordinates.Line, coordinates.Column);
            verificationActions(result, mainField, coordinates);
            return result;
        }
    }
}
