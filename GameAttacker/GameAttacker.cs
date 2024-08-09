using EntityLib;
using SwapLib;
using System.Collections.Generic;

namespace GameAttacker
{
    public class GameAttacker(Entity? firstEntity, Entity? secondEntity)
    {
        private static readonly Random rand = new Random();

        private Entity? attacker = null;
        private Entity? defender = null;

        private void distributionOfRoles()
        {
            if (rand.Next(1, 2) == 1)
            {
                attacker = firstEntity;
                defender = secondEntity;
            }
            else
            {
                attacker = secondEntity;
                defender = firstEntity;
            }
        }
        private bool calculateScore(Entity? entity) 
        {
            if(entity?.getMainField().boats.Count == entity?.getAmountDefeatBoat())
                return true;
            else
                return false;
        }
        public void startAttack()
        {
            distributionOfRoles();

            while (calculateScore(firstEntity) == false && 
                calculateScore(secondEntity) == false)
            {
                if (attacker is null || defender is null)
                    throw new Exception("attacker or defender is null");

                Console.Clear();
                Console.WriteLine($"Now attacking {attacker.Name}");
                Console.WriteLine("Press enter to continue!: ");
                Console.ReadLine();

                if (attacker.attack(defender.getMainField()) == false)
                    Swap<Entity?>.swap(ref attacker, ref defender);
            }
        }
    }
}
