using Builders;
using Coordinates2D;
using System.Numerics;
using TypeBoat;

namespace DirectorBuilders
{
    public class Director(IBuilder? builder)
    {
        public void setBuilder(IBuilder _builder) => builder = _builder;

        public Boat? creatBoat(Coordinates coordinates, DirectionAddition direction)
        {
            builder?.reset(coordinates, direction);
            return builder?.getResult();
        }
    }
}
