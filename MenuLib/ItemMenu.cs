using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuLib
{
    public struct ItemMenu<T>
    {
        public string textSelection;
        public T actionSelection;

        public ItemMenu(string textSelection, T actionSelection)
        {
            this.textSelection = textSelection;
            this.actionSelection = actionSelection;
        }
    }
}
