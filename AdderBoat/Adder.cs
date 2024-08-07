using Field;
using System.Numerics;
using BoatLib;
using BuilderBoatData;
using BuildersBoat;
using BuilderUse;
using Randomer;
using static Randomer.Randomer;

namespace AdderBoat
{
    public class Adder(MainField mainField)
    {
        private bool checkerFreePlace(Boat boat)
        {
            int emptyCellCounter = 0;
            foreach(PartBoat partBoat in boat.getPartBoats())
            {
                if (partBoat.Position.Line < 0 || partBoat.Position.Line >= mainField.fieldInfo.Line ||
                    partBoat.Position.Column < 0 || partBoat.Position.Column >= mainField.fieldInfo.Column)
                {
                    return false;
                }
                else if (mainField.field[partBoat.Position.Line][partBoat.Position.Column] == mainField.fieldInfo.EmptyCell)
                    emptyCellCounter++;
            }

            return emptyCellCounter == boat.Size ? true : false;
        }

        public bool addBoat(Boat? boat)
        {
            if (boat is null || checkerFreePlace(boat) == false)
            {
                Console.WriteLine("Add Error!");
                return false;
            }

            mainField.boats.Add(boat);
            mainField.updateFieldWithBoats();
            return true;
        }
        public bool addBoat(Boat[] boats)
        {
            int amountFreePlace = 0;
            foreach(Boat boat in boats)
            {
                if (checkerFreePlace(boat) == true)
                    amountFreePlace++;
            }

            if (amountFreePlace != boats.Length)
            {
                Console.WriteLine("Add Error!");
                return false;
            }

            mainField.boats.AddRange(boats);
            mainField.updateFieldWithBoats();
            return true;
        }
        public bool addBoat()
        {
            foreach (BuilderUsage<IBuilderBoat> builderUsage in BuilderData.builders)
            {
                for (int i = builderUsage.UsageCount; i > 0; --i)
                {
                    int attemptsAdd = 0;
                    bool successfulAdd = false;
                    while (attemptsAdd <= 10 && successfulAdd == false)
                    {
                        builderUsage.Builder.reset
                            (
                            randomCoordinates(mainField),
                            randomDirectionAddition()
                            );

                        successfulAdd = addBoat(builderUsage.Builder.getResult());
                        attemptsAdd++;
                    }
                }
            }

            return true;
        }

    }
}
