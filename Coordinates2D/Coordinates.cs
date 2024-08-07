using System;
using System.Xml.Linq;

namespace Coordinates2D
{
    public struct Coordinates : IEquatable<Coordinates>
    {
        public int Line {  get; set; }
        public int Column { get; set; }

        public Coordinates(int line, int column) 
        {      
            Line = line;
            Column = column;
        }

        public bool Equals(Coordinates other)
        {
            return Line == other.Line && Column == other.Column;
        }

        public override bool Equals(Object? obj)
        {
            if (obj is Coordinates other)
            {
                return Equals(other);
            }
            return false;
        }

        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return left.Equals(right);
        }

        // Перегрузка оператора !=
        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return !(left == right);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Line.GetHashCode();
                hash = hash * 31 + Column.GetHashCode();
                return hash;
            }
        }
        public override string ToString()
        {
            return $"Line: {Line}  |  Column: {Column}";
        }
    }
}
