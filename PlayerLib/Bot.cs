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

        private void fillFieldWithBoad()
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

        public void printField()
        {
            field.printField();
        }

    }
}
