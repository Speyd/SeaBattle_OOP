using Field;
using BoatLib;

namespace BuildersField
{
    public interface IBuilderField
    {
        protected const char emptyCell = '.';
        protected const char missCell = '0';
        protected const char shipDefeat = 'X';

        protected static readonly int Height;
        protected static readonly int Width;

        void reset(
            bool addBoat = true,
            char _emptyCell = IBuilderField.emptyCell,
            char _missCell = IBuilderField.missCell,
            char _shipDefeat = IBuilderField.shipDefeat
            );

        MainField? getResult();

    }
}
