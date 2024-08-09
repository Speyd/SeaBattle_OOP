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

        public Entity? firstEntity = null;
        public Entity? secondEntity = null;

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
        private Entity creationEntity(IBuilderField builder, List<Type> types, int index)
        {
            Entity? tempEntity = null;

            if (index < 0 || index >= types.Count)
                throw new Exception("Index out of range(List<Type> types - creationEntity())");


            if (types[index] == typeof(Player))
            {
                tempEntity = new Player(0, 0, false);

                Console.WriteLine($"Now you will edit the player field under number{index + 1}");
                Thread.Sleep(3000);
                creator.creat(ref tempEntity.getMainField(), builder, false);
            }
            else if (types[index] == typeof(Bot))
            {
                tempEntity = new Bot(0, 0);
                creator.creat(ref tempEntity.getMainField(), builder);
            }


            return tempEntity is null ? throw new Exception("tempEntity is null") : tempEntity;
        }

        public void start()
        {            
            List<Type> types = getTypeEntityList();

            Menu<IBuilderField> menum = new Menu<IBuilderField>("What field do you want to form?", getBuilderFieldList());
            IBuilderField builder = menum[menum.setChoicePlayer()];

            firstEntity = creationEntity(builder, types, 0);
            secondEntity = creationEntity(builder, types, 1);
        }
    }
}
