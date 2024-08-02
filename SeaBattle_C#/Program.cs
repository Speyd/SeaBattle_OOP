using Field;
using TypeBoat;
using AdderBoat;
using AttackerBoat;
using System.Numerics;

try
{
    MainField mainField = new MainField(2, 3);
    Boat boat = new Boat(3, new Vector2(0,0), DirectionAddition.LEFT, 'K');
    Adder adder = new Adder(mainField);
    Attacker attacker = new Attacker();
    adder.addBoat(boat);

    mainField.printField();
    attacker.attack(mainField, 1,0);
    Console.WriteLine("");
    mainField.printField(); 

}catch(Exception e)
{
    Console.WriteLine(e.Message);
}
