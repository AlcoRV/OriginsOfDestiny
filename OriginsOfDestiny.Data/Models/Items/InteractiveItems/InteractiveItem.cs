namespace OriginsOfDestiny.Data.Models.Items.InteractiveItems;

/// <summary>
///     Объекты взаимодействия
/// </summary>
public abstract class InteractiveItem
{
    public abstract Guid Id { get; set; }
    public abstract string Name { get; set; }
}
