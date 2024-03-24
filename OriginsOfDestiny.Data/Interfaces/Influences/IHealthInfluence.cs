using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.DataObjects.Interfaces.Influences
{
    public interface IHealthInfluence
    {
        public bool GetDamage(Damage damage);
        public void GetHealing(Heal heal);
    }
}
