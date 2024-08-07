using Field;
using Randomer;
using BoatLib;
using BuildersBoat;
using AdderBoat;
using AttackerBoat;
using Coordinates2D;
using BuilderUse;
using BuildersField;
using BuilderBoatData;
using static Randomer.Randomer;

namespace BotLib
{
    public class Bot
    {
        private static AttackerBot attacker = new AttackerBot();

        private List<Coordinates> lastAttack = new List<Coordinates>();
        private MainField field;
        private Adder adder;
        public int Score { get; set; } = 0;


        #region Constructor

        public Bot(IBuilderField builderField)
        {
            builderField.reset();
            this.field = builderField.getResult() ?? new MainField(6, 6);

            adder = new Adder(field);
        }
        public Bot(MainField field)
        {
            this.field = field;
            adder = new Adder(field);

            adder.addBoat();
        }
        public Bot(int line, int column, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X')
        {
            field = new MainField(line, column, emptyCell, missCell, shipDefeat);
            adder = new Adder(field);

            adder.addBoat();
        }
        #endregion

        #region Attack
        private void eventSuccessAttack(MainField mainField, ref Coordinates attackCoordinates)
        {
            Score++;

            Boat? boat = mainField.findBoat(attackCoordinates);
            if (boat is not null)
            {
                if (boat.Condition == ShipCondition.DESTROYED)
                    lastAttack.Clear();
                else
                    lastAttack.Add(attackCoordinates);
            }
        }
        private void eventMissAttack(MainField mainField)
        {
            DirectionAddition currentDirection = attacker.DeterminingDirectionOfMultiShip(ref lastAttack);

            Coordinates initialCoordinates = lastAttack[0];
            lastAttack.Add(initialCoordinates);

            for (int i = 0; i < mainField.fieldInfo.Column; i++)
                Boat.directionDetermination(currentDirection)(ref initialCoordinates);

            lastAttack.Insert(0, initialCoordinates);
        }
        public void attack(MainField mainField)
        {
            Coordinates attackCoordinates = new Coordinates(-1, -1);

            if (lastAttack.Count >= 2)
                attacker.findCoordinatesMultiShips(ref attackCoordinates, ref lastAttack);
            else
                attacker.findCoordinatesShip(mainField, ref attackCoordinates, ref lastAttack);


            if (attacker.attack(mainField, attackCoordinates) == CheckingResult.SUCCESS)
                eventSuccessAttack(mainField, ref attackCoordinates);
            else if (lastAttack.Count >= 2)
                eventMissAttack(mainField);
        }
        #endregion

        public MainField getMainField() => field;
        public void printField()
        {
            field.printField();
        }

    }
}
