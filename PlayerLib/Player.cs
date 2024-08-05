using AttackerBoat;
using Field;
using TypeBoat;
using AdderBoat;
using System.Numerics;
using System.Drawing;
using Coordinates2D;

namespace PlayerLib
{
    public class Player
    {
        private static Attacker attacker = new Attacker();

        private MainField field;
        private Adder adder;
        public int Score { get; set; } = 0;


        #region Constructor
        public Player(MainField field, Boat[]? boats = null)
        {
            this.field = field;

            adder = new Adder(field);
            if(boats is not null)
                adder.addBoat(boats);
        }

        public Player(int line, int column, Boat[]? boats = null,
                     char emptyCell = '.', char missCell = '0', char shipDefeat = 'X')
        {
            field = new MainField(line, column, emptyCell, missCell, shipDefeat);

            adder = new Adder(field);
            if (boats is not null)
                adder.addBoat(boats);
        }
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
        public void attack(MainField targetField, Coordinates coordinates)
        {
            if (attacker.attack(targetField, coordinates) == CheckingResult.SUCCESS)
                Score++;
        }
        #endregion

        public MainField getMainField() => field;

        public void printField()
        {
            field.printField();
        }
    }
}
