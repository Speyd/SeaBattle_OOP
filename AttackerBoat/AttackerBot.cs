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
        public void findCoordinatesMultiShips(ref Coordinates attackCoordinates, ref List<Coordinates> lastAttack)
        {
            attackCoordinates = lastAttack[lastAttack.Count - 1];
            Boat.directionDetermination(DeterminingDirectionOfMultiShip(ref lastAttack))(ref attackCoordinates);
        }
        public void findCoordinatesShip(MainField mainField, ref Coordinates attackCoordinates, ref List<Coordinates> lastAttack)
        {
            if (lastAttack.Count == 1)
            {

                CheckingResult checker1 = CheckingResult.NO_SUCCESS;
                DirectionAddition temp = DirectionAddition.LEFT;
                while (checker1 != CheckingResult.SUCCESS && lastAttackDirection.Count < (int)DirectionAddition.MAX)
                {
                    Coordinates tempCoo = lastAttack[0];
                    bool d = false;
                    while (d == false)
                    {
                        temp = randomDirectionAddition();
                        if (lastAttackDirection.Count == 0)
                            break;
                        for(int i = 0; i < lastAttackDirection.Count; ++i )
                        {
                            if (lastAttackDirection[i] == temp)
                                break;
                            if (i + 1 == lastAttackDirection.Count && lastAttackDirection[i] != temp)
                                d = true;
                        }
                    }
                    lastAttackDirection.Add(temp);
                    Boat.directionDetermination(temp)(ref tempCoo);

                    checker1 = attacker.checkingConditions(mainField, tempCoo.Line, tempCoo.Column);

                    if (checker1 != CheckingResult.NO_SUCCESS && checker1 != CheckingResult.ERROR)
                        attackCoordinates = tempCoo;

                }
            }
            else
            {

                CheckingResult checker = CheckingResult.NO_SUCCESS;
                while (checker == CheckingResult.NO_SUCCESS)
                {
                    attackCoordinates = randomCoordinates(mainField);
                    checker = attacker.checkingConditions(mainField, attackCoordinates.Line, attackCoordinates.Column);
                }
            }
        }

        public CheckingResult attack(MainField mainField, Coordinates coordinates)
        {
            return attacker.attack(mainField, coordinates);
        }
    }
}
