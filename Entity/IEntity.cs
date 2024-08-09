using static System.Formats.Asn1.AsnWriter;
using Field;
using AttackerBoat;
using AdderBoat;
using BuildersField;

namespace Entity
{
    public abstract class IEntity
    {
        protected static Attacker attacker = new Attacker();

        protected MainField field;
        protected Adder adder;
        protected int Score { get; set; } = 0;

        public IEntity(IBuilderField builderField, bool addBoats = true)
        {
            builderField.reset(addBoats);
            this.field = builderField.getResult() ?? new MainField(6, 6);

            adder = new Adder(field);
        }
        public IEntity(MainField field, bool addBoats = true)
        {
            this.field = field;
            adder = new Adder(field);

            if(addBoats == true)
                adder.addBoat();
        }
        public IEntity(
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

        public abstract MainField getMainField();
        public abstract void printField();
    }
}
