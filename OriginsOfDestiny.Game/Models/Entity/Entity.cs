using OriginsOfDestiny.Game.Enums;

namespace OriginsOfDestiny.Game.Models.Entity;

public abstract class Entity
{
    public string? Name { get; set; }
    public int HP { get; protected set; }
    public Gender Gender { get; protected set; }

    public void DamageSelf()
    {
        if (HP > 15)
        {
            HP -= 15;
        }
    }
}
