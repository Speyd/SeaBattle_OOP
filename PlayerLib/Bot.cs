using AdderBoat;
using AttackerBoat;
using Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TypeBoat;
using Builders;
using Coordinates2D;
using System.Collections;

namespace PlayerLib
{
    public class Bot
    {

        private static BuilderUsage[] builders =
        {
            new BuilderUsage ( new BattleshipBuilder(), 1 ),
            new BuilderUsage ( new CruiserBuilder(), 2 ),
            new BuilderUsage ( new DestroyerBuilder(), 3 ),
            new BuilderUsage ( new ShipBuilder(), 4 )
        };
        private static Random random = new Random();

        private List<Coordinates> lastAttack = new List<Coordinates>();
        private Attacker attacker = new Attacker();
        private MainField field;
        private Adder adder;

        public int Score { get; set; } = 0;


        #region Constructor
        public Bot(MainField field)
        {
            this.field = field;
            adder = new Adder(field);

            fillFieldWithBoad();
        }
        public Bot(int line, int column, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X')
        {
            field = new MainField(line, column, emptyCell, missCell, shipDefeat);
            adder = new Adder(field);

            fillFieldWithBoad();
        }
        #endregion

        private Coordinates getRandomCoordinates(MainField mainField)
        {
            int maxLine = mainField.fieldInfo.Line - 1;
            int maxColumn = mainField.fieldInfo.Column - 1;

            return new Coordinates(random.Next(0, maxLine), random.Next(0, maxColumn));
        }

        public void fillFieldWithBoad()
        {
            foreach(BuilderUsage builderUsage in builders)
            {
                for (; builderUsage.UsageCount > 0;)
                {
                    int attemptsAdd = 0;
                    bool useful = false;
                    while (attemptsAdd <= 3 && useful == false)
                    {
                        builderUsage.Builder.reset
                            (
                            getRandomCoordinates(field),
                            (DirectionAddition)random.Next(0, (int)DirectionAddition.MAX - 1)
                            );

                        useful = adder.addBoat(builderUsage.Builder.getResult());
                        attemptsAdd++;
                    }
                    builderUsage.UsageCount--;
                }
            }
        }

        #region Attack
        private DirectionAddition DeterminingDirectionOfMultiShip()
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
        private void attackMultiShips(ref Coordinates attackCoordinates)
        {  
            attackCoordinates = lastAttack[lastAttack.Count - 1];
            Boat.directionDetermination(DeterminingDirectionOfMultiShip())(ref attackCoordinates);
        }
        private void attackShip(MainField mainField, ref Coordinates attackCoordinates)
        {
            CheckingResult checker = CheckingResult.NO_SUCCESS;
            while (checker == CheckingResult.NO_SUCCESS)
            {
                attackCoordinates = getRandomCoordinates(mainField);
                checker = attacker.checkingConditions(mainField, attackCoordinates.Line, attackCoordinates.Column);
            }
        }
        private void eventSuccessAttack(MainField mainField, ref Coordinates attackCoordinates)
        {
            int indexBoat = mainField.findIndexBoat(attackCoordinates);
            if (indexBoat != -1)
            {
                int amountDefendPart = 0;
                for (int i = 0; i < mainField.boats[indexBoat].Size; i++)
                {
                    if (mainField.boats[indexBoat].getSymbol(i) == mainField.fieldInfo.ShipDefeat)
                        amountDefendPart++;
                }

                if (amountDefendPart == mainField.boats[indexBoat].Size)
                    lastAttack.Clear();
                else
                    lastAttack.Add(attackCoordinates);
            }
        }
        public void attack(MainField mainField)
        {
            Coordinates attackCoordinates = new Coordinates(-1, -1);

            if (lastAttack.Count >= 2)
                attackMultiShips(ref attackCoordinates);
            else
                attackShip(mainField, ref attackCoordinates);

            Console.WriteLine(attackCoordinates);
            if(attacker.attack(mainField, attackCoordinates) == CheckingResult.SUCCESS)
                eventSuccessAttack(mainField, ref attackCoordinates);
            else
                lastAttack.Clear();
        }
        #endregion

        public void printField()
        {
            field.printField();
        }

    }
}
