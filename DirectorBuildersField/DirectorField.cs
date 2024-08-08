using BuildersField;
using Coordinates2D;
using Field;

namespace DirectorBuildersField
{
    public class DirectorField(IBuilderField builder)
    {
        public void setBuilder(IBuilderField _builder) => builder = _builder;

        public MainField? creatField(bool autoAddBoat = true)
        {
            builder?.reset(autoAddBoat);
            return builder?.getResult();
        }
    }
}
