using MenuLib;
using Field;
using BuildersField;
using DirectorBuildersField;
using BoatLib;
using Coordinates2D;
using System.Drawing;
using AdderBoat;
using static MenuLib.DoubleChoices;
using static MenuLib.SelectionDimensions;


namespace FieldCreator
{
    public class Creator
    {
        private List<ItemMenu<IBuilderField>> getBuilderFieldList()
        {
            List<ItemMenu<IBuilderField>> builders = new List<ItemMenu<IBuilderField>>();

            builders.Add
                (
                new ItemMenu<IBuilderField>
                ($"Small field(Height: {SmallFieldBuilder.Height} | Width: {SmallFieldBuilder.Width})",
                new SmallFieldBuilder())
                );

            builders.Add
                (
                new ItemMenu<IBuilderField>
                ($"Medium field(Height: {MediumFieldBuilder.Height} | Width: {MediumFieldBuilder.Width})",
                new MediumFieldBuilder())
                );

            builders.Add
                (
                new ItemMenu<IBuilderField>
                ($"Large field(Height: {LargeFieldBuilder.Height} | Width: {LargeFieldBuilder.Width})",
                new LargeFieldBuilder())
                );

            return builders;
        }

        #region Selection
        private DirectionAddition selectionDirectionAddition()
        {
            Menu<DirectionAddition> menu = new Menu<DirectionAddition>
                (
                "In what direction do you want to add the ship?",
                new List<ItemMenu<DirectionAddition>>()
                {
                    new ItemMenu<DirectionAddition>("Up", DirectionAddition.UP),
                    new ItemMenu<DirectionAddition>("Down", DirectionAddition.DOWN),
                    new ItemMenu<DirectionAddition>("Right", DirectionAddition.RIGHT),
                    new ItemMenu<DirectionAddition>("Left", DirectionAddition.LEFT),
                }
                );

            return menu[menu.setChoicePlayer()];
        }
        private bool checkCorrectnessCoordinates
            (
            ref MainField mainField,
            ref Coordinates coordinates,
            int size, 
            DirectionAddition direction
            )
        {

            Adder adder = new Adder(mainField);

            if (coordinates.Line < 0 || coordinates.Line >= mainField.fieldInfo.Line ||
                   coordinates.Column < 0 || coordinates.Column >= mainField.fieldInfo.Column)
            {
                Console.Clear();
                Console.WriteLine("Your coordinates are out of bounds!");
                return false;
            }
            else if (adder.checkerFreePlace(new Boat(size, coordinates, direction, 'T')) == false)
            {
                Console.Clear();
                Console.WriteLine("There is no room for your ship!");
                return false;
            }

            return true;
        }

        private Coordinates? selectionCoordinates(ref MainField mainField, int size, DirectionAddition direction)
        {
            Coordinates coordinates = new Coordinates(-1, -1);

            while (true)
            {
                Console.Clear();

                mainField.printField();

                Console.Write("Enter coordinates by line: ");
                coordinates.Line = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter coordinates by column: ");
                coordinates.Column = int.Parse(Console.ReadLine() ?? "0");


                if (checkCorrectnessCoordinates(ref mainField, ref coordinates, size, direction) == true)
                    break;
                else
                {
                    Thread.Sleep(3000);
                    return null;
                }

            }

            return coordinates;
        }
        private char selectionSymbol()
        {
            Console.Clear();
            Console.Write("Enter the symbol of the boat: ");
            string symbol = Console.ReadLine() ?? "N";

            return symbol[0];
        }
        #endregion

        public Boat? creatBoat(ref MainField mainField)
        {
            int size;
            DirectionAddition direction;
            Coordinates? coordinates;
            char symbol;

            while (true)
            {
                symbol = selectionSymbol();
                size = selectionDimensions("Enter the size of the boat: ");
                direction = selectionDirectionAddition();
                coordinates = selectionCoordinates(ref mainField, size, direction);

                if (coordinates is null)
                {
                    if (doubleChoices("Do you want to remake this ship?") == false)
                    {
                        coordinates = null;
                        break;
                    }
                }
                else if(coordinates is not null)
                    break;

            }

            return coordinates.HasValue ? new Boat(size, coordinates.Value, direction, symbol) : null;
        }
        public void addBoats(ref MainField mainField)
        {
            int amoutBoats = selectionDimensions("How many boats will you add?: ");
            Adder adder = new Adder(mainField);

            for (int i = 1; i <= amoutBoats; i++)
            {
                Console.Clear();
                Console.WriteLine($"Now you will create {i} ship out of {amoutBoats}");
                Thread.Sleep(3000);

                Boat? boat = creatBoat(ref mainField);

                if (boat is null)
                    i--;
                else
                    adder.addBoat(boat);

                if (i < amoutBoats && doubleChoices($"Shall we add more ships(it remains to add {amoutBoats - i} boat(-s))?") == false)
                    return;
            }


        }
        private void creatField(ref MainField mainField)
        {
            Menu<IBuilderField> menu = new Menu<IBuilderField>("What field do you want to form?", getBuilderFieldList());
            DirectorField director = new DirectorField(menu[menu.setChoicePlayer()]);

            bool addAutomatBoats = doubleChoices("Add ships automatically?");
            mainField = director.creatField(addAutomatBoats) ?? throw new Exception("Null director.creatField()");

            if (addAutomatBoats == false)
                addBoats(ref mainField);
        }
        public void creat(ref MainField mainField)
        {
            creatField(ref mainField);
        }

    }
}
