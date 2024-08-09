using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuLib;

namespace BuildersField
{
    public class StandardListFiledBuilder
    {
        static public List<ItemMenu<IBuilderField>> getBuilderFieldList()
        {
            List<ItemMenu<IBuilderField>> builders = new List<ItemMenu<IBuilderField>>();

            builders.Add
                (
                new ItemMenu<IBuilderField>
                ($"Small field(Height: {SmallFieldBuilder.Height} | Width: {SmallFieldBuilder.Width})",
                new SmallFieldBuilder())
                );

            builders.Add
                (
                new ItemMenu<IBuilderField>
                ($"Medium field(Height: {MediumFieldBuilder.Height} | Width: {MediumFieldBuilder.Width})",
                new MediumFieldBuilder())
                );

            builders.Add
                (
                new ItemMenu<IBuilderField>
                ($"Large field(Height: {LargeFieldBuilder.Height} | Width: {LargeFieldBuilder.Width})",
                new LargeFieldBuilder())
                );

            return builders;
        }
    }
}
