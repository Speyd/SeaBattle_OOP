using Field;
using TypeBoat;
using AdderBoat;
using PlayerLib;
using AttackerBoat;
using System.Numerics;

try
{
    MainField mainField = new MainField(2, 3);
    Boat boat = new Boat(1, new Vector2(1,0), DirectionAddition.LEFT, 'K');
    Adder adder = new Adder(mainField);
    Attacker attacker = new Attacker();
    adder.addBoat(boat);

    Player player = new Player(2,2);
    player.addBoat(1, new Vector2(0,0), DirectionAddition.RIGHT, 'S');
    player.printField();
    Console.WriteLine(""); 
    Console.WriteLine(player.Score);

    mainField.printField();
    player.attack(mainField, 1,0);
    Console.WriteLine(player.Score);
    mainField.printField(); 

}catch(Exception e)
{
    Console.WriteLine(e.Message);
}
