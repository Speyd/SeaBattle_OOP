using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuLib
{
    public class DoubleChoices
    {
        static public bool doubleChoices(string textQuestion)
        {
            Menu<bool> menu = new Menu<bool>
                (
                    textQuestion,
                    new List<ItemMenu<bool>>()
                    {
                        new ItemMenu<bool> ("Yes", true),
                        new ItemMenu<bool> ("No", false)
                    }
                );

            return menu[menu.setChoicePlayer()];
        }
    }
}
