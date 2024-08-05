using Coordinates2D;
using Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeBoat;
using Randomer;
using static Randomer.Randomer;

namespace AttackerBoat
{
    public class AttackerBot
    {
        private static Attacker attacker = new Attacker();
        public DirectionAddition DeterminingDirectionOfMultiShip(ref List<Coordinates> lastAttack)
        {
            if (lastAttack[0].Column - lastAttack[1].Column == 0)
            {
                if (lastAttack[0].Line - lastAttack[1].Line == -1)
                    return DirectionAddition.DOWN;
                else return DirectionAddition.UP;
            }
            else if (lastAttack[0].Column - lastAttack[1].Column == -1)
                return DirectionAddition.RIGHT;
            else
                return DirectionAddition.LEFT;
        }
        public void findCoordinatesMultiShips(ref Coordinates attackCoordinates, ref List<Coordinates> lastAttack)
        {
            attackCoordinates = lastAttack[lastAttack.Count - 1];
            Boat.directionDetermination(DeterminingDirectionOfMultiShip(ref lastAttack))(ref attackCoordinates);
        }
        public void findCoordinatesShip(MainField mainField, ref Coordinates attackCoordinates)
        {
            CheckingResult checker = CheckingResult.NO_SUCCESS;
            while (checker == CheckingResult.NO_SUCCESS)
            {
                attackCoordinates = randomCoordinates(mainField);
                checker = attacker.checkingConditions(mainField, attackCoordinates.Line, attackCoordinates.Column);
            }
        }

        public CheckingResult attack(MainField mainField, Coordinates coordinates)
        {
            return attacker.attack(mainField, coordinates);
        }
    }
}
