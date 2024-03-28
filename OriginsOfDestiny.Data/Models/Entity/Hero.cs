using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.DataObjects.Interfaces.Models;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;

namespace OriginsOfDestiny.DataObjects.Models.Entity
{
    using Influences;
    using OriginsOfDestiny.DataObjects.Interfaces.Influences;

    public class Hero : IEntity, IMortal, IHasInventory, IDamageTo
    {
        public Gender Gender { get; set; } = Gender.Woman;
        public int MaxHP { get; set; } = 100;
        public int HP { get; set; } = 100;
        public string Name { get; set; }
        public IEnumerable<IItem> Inventory { get; set; } = new HashSet<IItem>();
        public Influences Influences { get; set; } = new Influences() { };
        public Damage Damage => new();
        public string KillMessage { get; set; }
        public Element Element { get; set; }

        public bool DamageTo(IMortal mortal)
        {
            return mortal.GetDamage(Damage);
        }

        public bool GetDamage(Damage damage)
        {
            HP -= (int)(damage.Value * Influences.Effects[damage.Element]);
            if (HP <= 0)
            {
                HP = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetHealing(Heal heal)
        {
            HP += (int)(heal.Value * Influences.Effects[heal.Element]);

            if (HP >= MaxHP) { 
                HP = MaxHP;
                return true;
            }

            return false;
        }
    }
}