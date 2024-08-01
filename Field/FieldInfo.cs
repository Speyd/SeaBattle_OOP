using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Field
{
    public class FieldInfo
    {
        public int Line { get; init; }
        public int Column { get; init; }
        public char EmptyCell { get; init; }
        public char MissCell {  get; init; }
        public char ShipDefeat { get; init; }


        public FieldInfo(int line, int column, char emptyCell = '.', char missCell = '0', char shipDefeat = 'X')
        {
            Line = line;
            Column = column;
            EmptyCell = emptyCell;
            MissCell = missCell;
            ShipDefeat = shipDefeat;
        }
    }
}
