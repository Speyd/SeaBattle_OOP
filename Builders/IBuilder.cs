using System.Numerics;
using TypeBoat;
using Coordinates2D;

namespace Builders
{
    public interface IBuilder
    {
        void reset(Coordinates coordinates, DirectionAddition direction);

        void setCoordinates(Coordinates coordinates);

        void setDirectionAddition(DirectionAddition direction);

        Boat? getResult();
    }
}
