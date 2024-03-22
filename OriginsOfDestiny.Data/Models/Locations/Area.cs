using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;

namespace OriginsOfDestiny.Data.Models.Locations;

public class Area: IHasPicture
{
    public string Name { get; set; }
    public Location Location { get; set; }
    public string Description { get; set; }
    public IEnumerable<Area> Paths { get; set; }
    public IEnumerable<IInteractiveItem> InteractiveItems { get; set; }

    public string Picture { get; set; }
}
