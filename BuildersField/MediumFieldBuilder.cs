using AdderBoat;
using Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersField
{
    public class MediumFieldBuilder : IBuilderField
    {
        private MainField? field = null;

        public static readonly int Height = 8;
        public static readonly int Width = 10;

        public void reset(
            bool addBoat = true,
            char _emptyCell = IBuilderField.emptyCell,
            char _missCell = IBuilderField.missCell,
            char _shipDefeat = IBuilderField.shipDefeat)
        {
            field = new MainField(Height, Width, _emptyCell, _missCell, _shipDefeat);
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
