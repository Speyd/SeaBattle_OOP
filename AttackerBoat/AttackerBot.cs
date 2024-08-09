using Coordinates2D;
using Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatLib;
using Randomer;
using static Randomer.Randomer;

namespace AttackerBoat
{
    public class AttackerBot
    {
        private static Attacker attacker = new Attacker();
        private List<DirectionAddition> lastAttackDirection = new List<DirectionAddition>();
        public DirectionAddition DeterminingDirectionOfMultiShip(ref List<Coordinates> lastAttack)
        {
            if (lastAttack[0].Column - lastAttack[1].Column == 0)
            {
                if (lastAttack[0].Line - lastAttack[1].Line <= -1)
                    return DirectionAddition.DOWN;
                else return DirectionAddition.UP;
            }
            else if (lastAttack[0].Column - lastAttack[1].Column <= -1)
                return DirectionAddition.RIGHT;
            else
                return DirectionAddition.LEFT;
        }
        public void findCoordinatesMultiShips(MainField mainField, ref Coordinates attackCoordinates, ref List<Coordinates> lastAttack)
        {
            attackCoordinates = lastAttack[lastAttack.Count - 1];
            Boat.directionDetermination(DeterminingDirectionOfMultiShip(ref lastAttack))(ref attackCoordinates);

            CheckingResult result = attacker.checkingConditions(mainField, attackCoordinates.Line, attackCoordinates.Column);

            if (lastAttack[0].Line != lastAttack[lastAttack.Count - 1].Line &&
               lastAttack[0].Column != lastAttack[lastAttack.Count - 1].Column ||
               result == CheckingResult.NO_SUCCESS || result == CheckingResult.ERROR)
            {
                lastAttack.Clear();
                findCoordinatesShip(mainField, ref attackCoordinates, ref lastAttack);
                return;
            }

        }

        #region OneAttackInHistoryAttackList
        private void searchForNewDirection(ref DirectionAddition tempDirection, bool checkCorrectDirection)
        {
            while (checkCorrectDirection == false)
            {
                tempDirection = randomDirectionAddition();
                if (lastAttackDirection.Count == 0)
                    break;

                for (int i = 0; i < lastAttackDirection.Count; ++i)
                {
                    if (lastAttackDirection[i] == tempDirection)
                        break;
                    if (i + 1 == lastAttackDirection.Count && lastAttackDirection[i] != tempDirection)
                        checkCorrectDirection = true;
                }
            }
        }
        private void eventSuccessSearchDirection(
            MainField mainField,
            ref Coordinates attackCoordinates,
            ref DirectionAddition tempDirection,
            ref Coordinates tempCoordinates,
            ref CheckingResult checker)
        {

            lastAttackDirection.Add(tempDirection);
            Boat.directionDetermination(tempDirection)(ref tempCoordinates);

            checker = attacker.checkingConditions(mainField, tempCoordinates.Line, tempCoordinates.Column);
            if (checker != CheckingResult.NO_SUCCESS && checker != CheckingResult.ERROR)
                attackCoordinates = tempCoordinates;
        }

        private void eventOneAttackInHistoryAttackList(MainField mainField, ref Coordinates attackCoordinates, ref List<Coordinates> lastAttack)
        {
            CheckingResult checker = CheckingResult.NO_SUCCESS;
            DirectionAddition tempDirection = DirectionAddition.LEFT;

            while (checker != CheckingResult.SUCCESS && lastAttackDirection.Count < (int)DirectionAddition.MAX)
            {
                Coordinates tempCoordinates = lastAttack[0];
                bool checkCorrectDirection = false;

                searchForNewDirection(ref tempDirection, checkCorrectDirection);
                eventSuccessSearchDirection(mainField, ref attackCoordinates, ref tempDirection, ref tempCoordinates, ref checker);
            }

            lastAttackDirection.Clear();
        }
        #endregion
        private void eventNoneAttackInHistoryAttackList(MainField mainField, ref Coordinates attackCoordinates)
        {
            CheckingResult checker = CheckingResult.NO_SUCCESS;

            while (checker == CheckingResult.NO_SUCCESS || checker == CheckingResult.ERROR)
            {
                attackCoordinates = randomCoordinates(mainField);
                checker = attacker.checkingConditions(mainField, attackCoordinates.Line, attackCoordinates.Column);
            }
        }

        public void findCoordinatesShip(MainField mainField, ref Coordinates attackCoordinates, ref List<Coordinates> lastAttack)
        {
            

            if (lastAttack.Count == 1)
                eventOneAttackInHistoryAttackList(mainField, ref attackCoordinates, ref lastAttack);
            else if(lastAttack.Count == 0)
                eventNoneAttackInHistoryAttackList(mainField, ref attackCoordinates);
        }

        public CheckingResult attack(MainField mainField, Coordinates coordinates)
        {
            return attacker.attack(mainField, coordinates);
        }
    }
}
