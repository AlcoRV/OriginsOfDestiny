using OriginsOfDestiny.DataObjects.Interfaces.Models;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;
using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.DataObjects.Interfaces.Influences
{
    public interface IDamageTo
    {
        public Damage Damage { get; }
        public bool DamageTo(IMortal mortal);
    }
}
