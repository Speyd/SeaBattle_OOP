using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Field;
using AdderBoat;
using BoatLib;

namespace BuildersField
{
    public class SmallFieldBuilder : IBuilderField
    {

        private MainField? field = null;
        public void reset(
            bool addBoat = true,
            char _emptyCell = IBuilderField.emptyCell,
            char _missCell = IBuilderField.missCell,
            char _shipDefeat = IBuilderField.shipDefeat)
        {
            field = new MainField(6, 6, _emptyCell, _missCell, _shipDefeat);
            Adder adder = new Adder(field);

            if (addBoat)
                adder.addBoat();
        }
        public MainField? getResult()
        {
            MainField? tempField = field;
            field = null;
            return tempField;
        }
    }
}
