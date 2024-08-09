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

        public string Name {  get; init; }
        public int Score { get; set; } = 0;

        public Entity(Entity entity)
        {
            field = entity.getMainField();
            adder = new Adder(field);

            Name = entity.Name;
            Score = entity.Score;
        }
        public Entity(IBuilderField builderField, string name, bool addBoats = true)
        {
            Name = name;

            builderField.reset(addBoats);
            this.field = builderField.getResult() ?? new MainField(6, 6);

            adder = new Adder(field);
        }
        public Entity(MainField field, string name, bool addBoats = true)
        {
            Name = name;

            this.field = field;
            adder = new Adder(field);

            if (addBoats == true)
                adder.addBoat();
        }
        public Entity(
            int line, int column,
            string name,
            bool addBoats = true,
            char emptyCell = '.',
            char missCell = '0',
            char shipDefeat = 'X')
        {
            Name = name;

            field = new MainField(line, column, emptyCell, missCell, shipDefeat);
            adder = new Adder(field);

            if (addBoats == true)
                adder.addBoat();
        }

        public abstract bool attack(MainField mainField);
        public abstract int getAmountDefeatBoat();
        public abstract ref MainField getMainField();
        public abstract void printField();
    }
}
