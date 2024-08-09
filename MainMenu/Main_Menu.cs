using FieldCreator;
using MenuLib;
using BotLib;
using EntityLib;
using PlayerLib;
using BuildersField;
using DirectorBuildersField;
using System;
using static BuildersField.StandardListFiledBuilder;

namespace MainMenu
{
    public class Main_Menu
    {
        private static Creator creator = new Creator();
        private List<Type> getTypeEntityList()
        {
            Menu<List<Type>> menu = new Menu<List<Type>>
                (
                "Who will play?",
                new List<ItemMenu<List<Type>>>()
                {
                    new ItemMenu<List<Type>>
                    (
                        "Player and Bot",
                        new List<Type>(){ typeof(Player), typeof(Bot)}
                    ),
                    new ItemMenu<List<Type>>
                    (
                        "Player and Player",
                        new List<Type>(){ typeof(Player), typeof(Player) }
                    ),
                    new ItemMenu<List<Type>>
                    (
                        "Bot and Bot",
                        new List<Type>(){ typeof(Bot), typeof(Bot)}
                    ),
                }
                );

            return menu[menu.setChoicePlayer()];
        }
        private Entity? creationEntity(IBuilderField builder, List<Type> types, int index)
        {
            if (index < 0 || index >= types.Count)
                throw new Exception("Index out of range(List<Type> types - creationEntity())");


            if (types[index] == typeof(Player))
            {
                Player player = new Player(0, 0, $"Player #{index + 1}", false);

                Console.Clear();
                Console.WriteLine($"Now you will edit the player field under number {index + 1}");
                Thread.Sleep(3000);

                creator.creat(ref player.getMainField(), builder, false);

                return player;
            }

            else if (types[index] == typeof(Bot))
            {
                Bot bot = new Bot(0, 0, $"Bot #{index + 1}");
                creator.creat(ref bot.getMainField(), builder);

                return bot;
            }

            return null;
        }

        public void start(ref Entity? firstEntity, ref Entity? secondEntity)
        {            
            List<Type> types = getTypeEntityList();

            Menu<IBuilderField> menum = new Menu<IBuilderField>("What field do you want to form?", getBuilderFieldList());
            IBuilderField builder = menum[menum.setChoicePlayer()];

            firstEntity = creationEntity(builder, types, 0);
            secondEntity = creationEntity(builder, types, 1);
        }
    }
}
