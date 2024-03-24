using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Influences;
using OriginsOfDestiny.DataObjects.Models.Influences;


namespace OriginsOfDestiny.DataObjects.Interfaces.Models
{
    public interface IEntity
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        //public Influences Influences { get; set; }
    }
}
