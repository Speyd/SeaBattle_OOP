using AttackerBoat;
using Field;
using TypeBoat;
using AdderBoat;
using System.Numerics;
using System.Drawing;

namespace PlayerLib
{
    public class Player
    {
        private Attacker attacker = new Attacker();
        private MainField field;

        private Adder adder;
        public int Score { get; set; } = 0;


        #region Constructor
        public Player(Boat[] boats, MainField field)
        {
            this.field = field;

            adder = new Adder(field);
            adder.addBoats(boats);
        }
        public Player(MainField field, int amountBoat = 0)
            :this(new Boat[amountBoat], field)
        {}

        public Player(Boat[] boats, int line, int column, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X')
        {
            field = new MainField(line, column, emptyCell, missCell, shipDefeat);

            adder = new Adder(field);
            adder.addBoat(boats);
        }
        public Player(int line, int column, int amountBoat = 0, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X') 
            : this(new Boat[amountBoat], new MainField(line, column, emptyCell, missCell, shipDefeat))
        {}
        #endregion

        #region AddBoat
        public void addBoat(int size, Vector2 coordinates, DirectionAddition direction, char symbol)
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
        public void attack(MainField targetField, Vector2 coordinates)
        {
            if (attacker.attack(targetField, coordinates) == true)
                Score++;
        }
        public void attack(MainField targetField, int line, int column)
        {
            if(attacker.attack(targetField, line, column) == true) 
                Score++;
        }
        #endregion
    }
}
