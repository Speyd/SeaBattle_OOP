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

        public void attack(MainField targetField, Coordinates coordinates)
        {
            if (attacker.attack(targetField, coordinates) == CheckingResult.SUCCESS)
                Score++;
        }

        public override ref MainField getMainField() => ref field;

        public override void printField()
        {
            field.printField();
        }
    }
}
