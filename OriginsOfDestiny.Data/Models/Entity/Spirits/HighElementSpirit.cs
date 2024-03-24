using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Models;
using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.DataObjects.Models.Entity.Spirits
{
    using Influences;

    public class HighElementSpirit : IOpponent
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Attitude Attitude { get; set; }

        public string Picture { get; set; }
        public Influences Influences { get; set; }

        public bool DamageTo(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool GetDamage(Damage damage)
        {
            throw new NotImplementedException();
        }

        public void GetHealing(Heal heal)
        {

        }
    }
}
