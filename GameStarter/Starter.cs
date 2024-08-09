using EntityLib;
using MainMenu;
using GameAttacker;

namespace GameStarter
{
    public class Starter
    {
        private readonly Entity? firstEntity = null;
        private readonly Entity? secondEntity = null;

        private static readonly Main_Menu mainMenu = new Main_Menu();
        private readonly GameAttacker.GameAttacker gameAttacker;

        public Starter()
        {
            mainMenu.start(ref firstEntity, ref secondEntity);

            gameAttacker = new GameAttacker.GameAttacker(firstEntity, secondEntity);
        }

        private void determiningWinner()
        {
            Console.Clear();

            if (firstEntity?.Score > secondEntity?.Score)
                Console.WriteLine("firstEntity WINNER!");
            else if (secondEntity?.Score > firstEntity?.Score)
                Console.WriteLine("secondEntity WINNER!");
            else
                Console.WriteLine("DRAW!");
        }

        public void startGame()
        {
            gameAttacker.startAttack();
            determiningWinner();
        }
    }
}
