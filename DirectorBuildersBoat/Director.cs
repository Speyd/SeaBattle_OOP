using BuildersBoat;
using Coordinates2D;
using System.Numerics;
using BoatLib;

namespace DirectorBuildersBoat
{
    public class Director(IBuilderBoat? builder)
    {
        public void setBuilder(IBuilderBoat _builder) => builder = _builder;

        public Boat? creatBoat(Coordinates coordinates, DirectionAddition direction)
        {
            builder?.reset(coordinates, direction);
            return builder?.getResult();
        }
    }
}
