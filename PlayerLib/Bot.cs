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
        private static int l = 2;

        private List<Coordinates> lastAttack = new List<Coordinates>() 
        { 
            new Coordinates (1, 0),
            new Coordinates (1, 1),
        };
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
                            new Coordinates(random.Next(0, field.fieldInfo.Line), random.Next(0, field.fieldInfo.Column)),
                            (DirectionAddition)random.Next(0, (int)DirectionAddition.MAX - 1)
                            );

                        useful = adder.addBoat(builderUsage.Builder.getResult());
                        attemptsAdd++;
                    }
                    builderUsage.UsageCount--;
                }
            }
        }

        public void attack(MainField mainField)
        {
            Coordinates attackCoordinates = new Coordinates(-1, -1);

            if (lastAttack.Count >= 2)
            {
                DirectionAddition attack;

                if(lastAttack[0].Column - lastAttack[1].Column == 0)
                {
                    if (lastAttack[0].Line - lastAttack[1].Line == -1)
                        attack = DirectionAddition.DOWN; 
                    else attack = DirectionAddition.UP;
                }
                else if(lastAttack[0].Column - lastAttack[1].Column == -1)
                    attack = DirectionAddition.RIGHT;
                else
                    attack = DirectionAddition.LEFT;


                attackCoordinates = lastAttack[lastAttack.Count - 1];
                Boat.directionDetermination(attack)(ref attackCoordinates);
            }
            else
            {
                CheckingResult checker = CheckingResult.NO_SUCCESS;
                while (checker == CheckingResult.NO_SUCCESS)
                {
                    attackCoordinates = new Coordinates(random.Next(0, mainField.fieldInfo.Line - 1), random.Next(0, mainField.fieldInfo.Column - 1));
                    checker = attacker.checkingConditions(mainField, attackCoordinates.Line, attackCoordinates.Column);
                }
            }

            Console.WriteLine(attackCoordinates);
            if(attacker.attack(mainField, attackCoordinates) == CheckingResult.SUCCESS)
            {
                int indexBoat = mainField.findIndexBoat(attackCoordinates);
                if(indexBoat != -1) 
                {
                    int amountDefendPart = 0;
                    for(int i = 0; i < mainField.boats[indexBoat].Size; i++)
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
            else
                lastAttack.Clear();
        }

        public void printField()
        {
            field.printField();
        }

    }
}
