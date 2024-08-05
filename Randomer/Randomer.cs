using System;
using Coordinates2D;
using Field;
using TypeBoat;


namespace Randomer
{
    public class Randomer
    {
        private static Random random = new Random();
        static public Coordinates randomCoordinates(MainField mainField)
        {
            int maxLine = mainField.fieldInfo.Line - 1;
            int maxColumn = mainField.fieldInfo.Column - 1;

            return new Coordinates(random.Next(0, maxLine), random.Next(0, maxColumn));
        }

        static public DirectionAddition randomDirectionAddition() 
        {
            return (DirectionAddition)random.Next(0, (int)DirectionAddition.MAX - 1);             
        }
    }
}
