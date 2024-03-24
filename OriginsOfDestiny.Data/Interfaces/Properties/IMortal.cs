using OriginsOfDestiny.DataObjects.Interfaces.Influences;

namespace OriginsOfDestiny.DataObjects.Interfaces.Properties
{
    public interface IMortal: IHealthInfluence
    {
        public int MaxHP { get; set; }
        public int HP { get; set; }
    }
}
