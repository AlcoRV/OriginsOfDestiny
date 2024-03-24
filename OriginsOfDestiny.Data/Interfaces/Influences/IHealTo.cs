using OriginsOfDestiny.DataObjects.Interfaces.Properties;
using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.DataObjects.Interfaces.Influences
{
    public interface IHealTo
    {
        public Heal Heal { get; }
        public void HealTo(IMortal mortal);
    }
}
