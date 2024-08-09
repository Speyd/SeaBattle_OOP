using AttackerBoat;
using Field;
using BoatLib;
using AdderBoat;
using System.Numerics;
using System.Drawing;
using Coordinates2D;
using BuildersField;
using EntityLib;

namespace PlayerLib
{
    public class Player : Entity
    {
        protected static Attacker attacker = new Attacker();

        #region Constructor

        public Player(IBuilderField builder, bool addBoat = false) 
            : base(builder, addBoat)
        { }
        public Player(MainField field, bool addBoat = false) 
            :base(field, addBoat)
        { }

        public Player(
            int line, int column,
            bool addBoat = false,
            char emptyCell = '.', 
            char missCell = '0',
            char shipDefeat = 'X')
            :base(line, column, addBoat, emptyCell, missCell, shipDefeat)
        {}
        #endregion

        #region AddBoat
        public void addBoat(int size, Coordinates coordinates, DirectionAddition direction, char symbol)
        {
            adder.addBoat(new Boat(size, coordinates, direction, symbol));
        }
        public void addBoat(Boat boat)
        {
            adder.addBoat(boat);
        }
        public void addBoat(Boat[] boats)
        {
            adder.addBoat(boats);
        }
        #endregion


        #region Attack
        public bool attack(MainField mainField, Coordinates coordinates)
        {
            if (attacker.attack(mainField, coordinates) == CheckingResult.SUCCESS)
            {
                Score++;
                return true;
            }

            return false;
        }   
        public override bool attack(MainField mainField)
        {
            Coordinates coordinates = new Coordinates(-1, -1);

            while (attacker.checkingConditions(mainField, coordinates.Line, coordinates.Column) == CheckingResult.NO_SUCCESS ||
                attacker.checkingConditions(mainField, coordinates.Line, coordinates.Column) == CheckingResult.ERROR)
            {
                Console.Clear();
                mainField.printField();

                Console.Write("Enter coordinates by line: ");
                coordinates.Line = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter coordinates by column: ");
                coordinates.Column = int.Parse(Console.ReadLine() ?? "0");
            }

            return attack(mainField, coordinates);
        }
        #endregion

        public override ref MainField getMainField() => ref field;

        public override int getAmountDefeatBoat()
        {
            int amountDefeatBoat = 0;
            foreach(Boat boat in field.boats)
            {
                if(boat.Condition == ShipCondition.DESTROYED)
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
