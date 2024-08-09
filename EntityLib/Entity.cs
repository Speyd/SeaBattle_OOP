using static System.Formats.Asn1.AsnWriter;
using Field;
using AttackerBoat;
using AdderBoat;
using BuildersField;

namespace EntityLib
{
    public abstract class Entity
    {
        protected MainField field;
        protected Adder adder;
        protected int Score { get; set; } = 0;

        public Entity(IBuilderField builderField, bool addBoats = true)
        {
            builderField.reset(addBoats);
            this.field = builderField.getResult() ?? new MainField(6, 6);

            adder = new Adder(field);
        }
        public Entity(MainField field, bool addBoats = true)
        {
            this.field = field;
            adder = new Adder(field);

            if (addBoats == true)
                adder.addBoat();
        }
        public Entity(
            int line, int column,
            bool addBoats = true,
            char emptyCell = '.',
            char missCell = '0',
            char shipDefeat = 'X')
        {
            field = new MainField(line, column, emptyCell, missCell, shipDefeat);
            adder = new Adder(field);

            if (addBoats == true)
                adder.addBoat();
        }

        public abstract ref MainField getMainField();
        public abstract void printField();
    }
}
