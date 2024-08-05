using Field;
using Randomer;
using TypeBoat;
using Builders;
using AdderBoat;
using AttackerBoat;
using Coordinates2D;
using static Randomer.Randomer;

namespace BotLib
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
        private static AttackerBot attacker = new AttackerBot();

        private List<Coordinates> lastAttack = new List<Coordinates>();
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
            foreach (BuilderUsage builderUsage in builders)
            {
                for (; builderUsage.UsageCount > 0;)
                {
                    int attemptsAdd = 0;
                    bool useful = false;
                    while (attemptsAdd <= 3 && useful == false)
                    {
                        builderUsage.Builder.reset
                            (
                            randomCoordinates(field),
                            randomDirectionAddition()        
                            );

                        useful = adder.addBoat(builderUsage.Builder.getResult());
                        attemptsAdd++;
                    }
                    builderUsage.UsageCount--;
                }
            }
        }

        #region Attack
        private void eventSuccessAttack(MainField mainField, ref Coordinates attackCoordinates)
        {
            Score++;

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
                attacker.findCoordinatesMultiShips(ref attackCoordinates, ref lastAttack);
            else
                attacker.findCoordinatesShip(mainField, ref attackCoordinates);


            if (attacker.attack(mainField, attackCoordinates) == CheckingResult.SUCCESS)
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
