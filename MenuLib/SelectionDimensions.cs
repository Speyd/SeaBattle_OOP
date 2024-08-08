using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuLib
{
    public class SelectionDimensions
    {
        static public int selectionDimensions(string textQuestion)
        {
            Console.Clear();
            Console.Write(textQuestion);
            int size = int.Parse(Console.ReadLine() ?? "1");
            if (size <= 0)
                size = 1;

            return size;
        }
    }
}
