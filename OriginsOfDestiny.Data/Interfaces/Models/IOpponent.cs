using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;

namespace OriginsOfDestiny.DataObjects.Interfaces.Models
{
    public interface IOpponent: IEntity, IHasPicture
    {
        public Attitude Attitude { get; set; }
    }
}
