using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.DataObjects.Interfaces.Influences
{
    public interface IHealthInfluence
    {
        /// <summary>
        ///     Get damage
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>true, if object is dead</returns>
        public bool GetDamage(Damage damage);

        /// <summary>
        ///     Get healing
        /// </summary>
        /// <param name="heal"></param>
        /// <returns>true, if object has full health</returns>
        public bool GetHealing(Heal heal);
    }
}
