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
using EntityLib;
using static Randomer.Randomer;

namespace BotLib
{
    public class Bot : Entity
    {
        protected static AttackerBot attacker = new AttackerBot();
        private List<Coordinates> lastAttack = new List<Coordinates>();


        #region Constructor

        public Bot(IBuilderField builderField) 
            :base(builderField, true)
        { }
        public Bot(MainField field) 
            :base(field, true)
        { }
        public Bot(int line, int column, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X') 
            :base(line, column, true, emptyCell, missCell, shipDefeat)
        { }
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
        public override bool attack(MainField mainField)
        {
            Coordinates attackCoordinates = new Coordinates(-1, -1);

            if (lastAttack.Count >= 2)
                attacker.findCoordinatesMultiShips(ref attackCoordinates, ref lastAttack);
            else
                attacker.findCoordinatesShip(mainField, ref attackCoordinates, ref lastAttack);

            if (attacker.attack(mainField, attackCoordinates) == CheckingResult.SUCCESS)
            {
                eventSuccessAttack(mainField, ref attackCoordinates);
                return true;
            }
            else if (lastAttack.Count >= 2)
                eventMissAttack(mainField);


            return false;
        }
        #endregion

        public override ref MainField getMainField() => ref field;
        public override int getAmountDefeatBoat()
        {
            int amountDefeatBoat = 0;
            foreach (Boat boat in field.boats)
            {
                if (boat.Condition == ShipCondition.DESTROYED)
                    amountDefeatBoat++;
            }

            return amountDefeatBoat;
        }
        public override void printField()
        {
            field.printField();
        }

    }
}
