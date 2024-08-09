using EntityLib;
using MainMenu;
using GameAttacker;
using System;

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

        #region Show
        private void showDetailsField(Entity? entity)
        {
            Console.WriteLine($"Field firstEntity(Score: {entity?.Score}):");
            Console.WriteLine($"Destruction of ships: {entity?.getAmountDefeatBoat()}");

            entity?.printField();
        }
        private void showAllField()
        {
            Console.Clear();
            showDetailsField(firstEntity);

            Console.WriteLine("\n");

            showDetailsField(secondEntity);
        }
        private void determiningWinner()
        {
            Console.Clear();

            if (firstEntity?.Score > secondEntity?.Score)
                Console.WriteLine($"firstEntity WINNER(Score: {firstEntity.Score})!");
            else if (secondEntity?.Score > firstEntity?.Score)
                Console.WriteLine($"secondEntity WINNER!(Score: {secondEntity.Score})");
            else
            {
                Console.WriteLine("DRAW!");
                Console.WriteLine($"firstEntity Score: {firstEntity?.Score}");
                Console.WriteLine($"secondEntity Score: {secondEntity?.Score}");
            }

            Console.WriteLine("Press enter to continue!: ");
            Console.ReadLine();
        }
        #endregion

        public void startGame()
        {
            gameAttacker.startAttack();
            determiningWinner();
            showAllField();
        }
    }
}
