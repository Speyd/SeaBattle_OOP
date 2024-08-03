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

namespace PlayerLib
{
    public class Bot
    {

        private Attacker attacker = new Attacker();
        private MainField field;
        private static Random random = new Random();
        private Adder adder;
        public int Score { get; set; } = 0;


        #region Constructor
        public Bot(MainField field)
        {
            this.field = field;
            adder = new Adder(field);

            //fillFieldWithBoad();
        }

        public Bot(int line, int column, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X')
        {
            field = new MainField(line, column, emptyCell, missCell, shipDefeat);
            adder = new Adder(field);

           // fillFieldWithBoad();
        }
        #endregion

        //4-1 3-2 2-3 1-4
        //private void fillFieldWithBoad()
        //{
          
        //    for(int i = 0; i < 6; i++)
        //    {
        //        int attemptsAdd = 0;
        //        bool checkFreePlace = false;
        //        while(attemptsAdd < 3 && checkFreePlace == false)
        //        {
        //            checkFreePlace = adder.addBoat(new Boat(2, new Vector2(random.Next(0, field.fieldInfo.Line), random.Next(0, field.fieldInfo.Column)), DirectionAddition.RIGHT, 'K'));
        //            if (checkFreePlace == false)
        //                attemptsAdd++;
        //        }
        //    }
        //}

        public void printField()
        {
            field.printField();
        }

    }
}
