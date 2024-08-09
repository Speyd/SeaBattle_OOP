using BotLib;
using Field;
using BoatLib;
using BuildersBoat;
using AdderBoat;
using PlayerLib;
using AttackerBoat;
using FieldCreator;
using Coordinates2D;
using BuildersField;
using System.Numerics;
using DirectorBuildersBoat;
using MainMenu;
using GameStarter;
try
{

    //IBuilderBoat builder = new BattleshipBuilder();
    //Director director = new Director(builder);
    //Boat? boat_1 = director.creatBoat(new Coordinates(1, 0), DirectionAddition.LEFT);
    //Console.WriteLine($"Vector2: {boat_1?.getCoordinate(boat_1.Size - 1)}\nDirectionAddition: {boat_1?.DirectionAdd.ToString()}");


    //Bot bot = new Bot(new LargeFieldBulder());
    //Console.WriteLine("BOT:");
    //bot.printField();
    //Console.WriteLine("");
    //Console.WriteLine("");
    //MainField mainField = new MainField(2, 3);

    //CruiserBuilder battleshipBuilder = new CruiserBuilder();
    //BattleshipBuilder b = new BattleshipBuilder();
    //b.reset(new Coordinates(1, 0), DirectionAddition.RIGHT);
    //battleshipBuilder.reset(new Coordinates(1, 0), DirectionAddition.RIGHT);
    //Boat? boat = battleshipBuilder.getResult();

    //Adder adder = new Adder(mainField);
    //Attacker attacker = new Attacker();
    //if(boat is not null)
    //    adder.addBoat(boat);

    //Player player = new Player(3, 4);
    //battleshipBuilder.reset(new Coordinates(1, 0), DirectionAddition.UP);
    //Boat? h = b.getResult();

    //if (h is not null)
    //    player.addBoat(h);
    ////attacker.attack(player.getMainField(), new Coordinates(1, 0));
    //attacker.attack(player.getMainField(), new Coordinates(1, 1));

    //player.printField();
    //Console.WriteLine("");
    ////Console.WriteLine(player.Score);
    //bot.attack(player.getMainField());
    //bot.attack(player.getMainField());
    //bot.attack(player.getMainField());
    //bot.attack(player.getMainField());

    ////bot.attack(player.getMainField());
    ////bot.attack(player.getMainField());

    //Console.WriteLine("");
    //player.printField();
    ////mainField.printField();
    ////player.attack(mainField, 1, 0);
    ////Console.WriteLine(player.Score);
    ////mainField.printField();
    ///


    //Player player = new Player(1, 1);
    //player.addBoat(1, new Coordinates(0, 0), DirectionAddition.LEFT, 'C');
    //Bot bot = new Bot(new LargeFieldBuilder());

    //player.printField();
    //bot.attack(player.getMainField());
    //player.printField();

    //MainField mainField = new MainField(2, 3);
    //mainField.printField();
    //Console.ReadLine();
    //Creator creator = new Creator();
    //creator.creat(ref mainField);

    //mainField.printField();


    //Console.WriteLine("");
    // Console.WriteLine("");

    //bot.printField();
    //player.attack(bot.getMainField(), new Coordinates(1, 2));
    //bot.printField();
    //Main_Menu mainMenu = new Main_Menu();
    //mainMenu.start();

    Starter starter = new Starter();
    starter.startGame();

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
