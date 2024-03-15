using OriginsOfDestiny.Data.Models.Items.InteractiveItems;

namespace OriginsOfDestiny.Data.Models.Locations;

public class Area
{
    public string Name { get; set; }
    public Location Location { get; set; }
    public string Description { get; set; }
    public IEnumerable<Area> Paths { get; set; }
    public IEnumerable<InteractiveItem> InteractiveItems { get; set; }
}
