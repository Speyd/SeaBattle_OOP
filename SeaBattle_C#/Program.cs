using Field;
using TypeBoat;
using Builders;
using AdderBoat;
using PlayerLib;
using AttackerBoat;
using Coordinates2D;
using System.Numerics;
using DirectorBuilders;

try
{

    IBuilder builder = new BattleshipBuilder();
    Director director = new Director(builder);
    Boat? boat_1 = director.creatBoat(new Coordinates(1, 0), DirectionAddition.LEFT);
    Console.WriteLine($"Vector2: {boat_1?.getCoordinate(boat_1.Size - 1)}\nDirectionAddition: {boat_1?.DirectionAdd.ToString()}");


    //Bot bot = new Bot(6, 6);
    Console.WriteLine("BOT:");
    //bot.printField();
    Console.WriteLine("");
    Console.WriteLine("");
    MainField mainField = new MainField(2, 3);

    CruiserBuilder battleshipBuilder = new CruiserBuilder();
    battleshipBuilder.reset(new Coordinates(1, 0), DirectionAddition.RIGHT);
    Boat? boat = battleshipBuilder.getResult();

    Adder adder = new Adder(mainField);
    Attacker attacker = new Attacker();
    if(boat is not null)
        adder.addBoat(boat);

    Player player = new Player(2, 2);
    player.addBoat(1, new Coordinates(0, 0), DirectionAddition.RIGHT, 'S');
    player.printField();
    Console.WriteLine("");
    Console.WriteLine(player.Score);

    mainField.printField();
    player.attack(mainField, 1, 0);
    Console.WriteLine(player.Score);
    mainField.printField();

}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
