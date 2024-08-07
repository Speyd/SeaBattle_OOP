using BuilderUse;
using BuildersBoat;

namespace BuilderBoatData
{
    public static class BuilderData
    {
        public static readonly BuilderUsage<IBuilderBoat>[] builders =
        {
            new BuilderUsage<IBuilderBoat>(new BattleshipBuilder(), 1),
            new BuilderUsage<IBuilderBoat>(new CruiserBuilder(), 2),
            new BuilderUsage<IBuilderBoat>(new DestroyerBuilder(), 3),
            new BuilderUsage<IBuilderBoat>(new ShipBuilder(), 4)
        };
    }
}
