using OriginsOfDestiny.Data.Enums;

namespace OriginsOfDestiny.DataObjects.Interfaces.Models
{
    public interface IEntity
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
}
