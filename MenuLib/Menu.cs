using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MenuLib
{
    public class Menu<T>
    {
        private int choice;
        private string textQuestion;
        private List<ItemMenu<T>> items = new List<ItemMenu<T>>();

        public Menu(string _textQuestion, List<ItemMenu<T>> _items)
        {
            items = _items;
            textQuestion = _textQuestion;
        }

        public void inputItemMenu()
        {
            for(int i = 0; i < items.Count; ++i)
            {
                Console.WriteLine($"{i + 1}. {items[i].textSelection}");
            }
        }

        public int setChoicePlayer()
        {
            choice = 1;

            if (items.Count == 0) 
                return -1;

            do
            {
                Console.Clear();

                inputItemMenu();

                Console.Write($"{textQuestion}: ");
                choice = int.Parse(Console.ReadLine() ?? "-1");

            } while (choice < 1 || choice > items.Count);

            return --choice;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= items.Count)
                    throw new IndexOutOfRangeException("Index out of range");
                return items[index].actionSelection;
            }
        }
    }
}
