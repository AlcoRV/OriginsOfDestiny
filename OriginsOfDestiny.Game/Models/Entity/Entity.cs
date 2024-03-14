using OriginsOfDestiny.Game.Enums;

namespace OriginsOfDestiny.Game.Models.Entity;

public abstract class Entity
{
    public string? Name { get; set; }
    public virtual int HP { get; set; }
    public virtual Gender Gender { get; set; }

    public void DamageSelf()
    {
        if (HP > 15)
        {
            HP -= 15;
        }
    }
}
