using OriginsOfDestiny.DataObjects.Interfaces.Properties;
using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.DataObjects.Interfaces.Influences
{
    public interface IHealTo
    {
        public Heal Heal { get; }
        /// <summary>
        ///     To heal
        /// </summary>
        /// <param name="mortal"></param>
        /// <returns>true, if healed full</returns>
        public bool HealTo(IMortal mortal);
    }
}
