using System.Numerics;
using BoatLib;
using Coordinates2D;

namespace BuildersBoat
{
    public interface IBuilderBoat
    {
        void reset(Coordinates coordinates, DirectionAddition direction);

        void setCoordinates(Coordinates coordinates);

        void setDirectionAddition(DirectionAddition direction);

        Boat? getResult();
    }
}
