namespace OriginsOfDestiny.Game.Models;

public abstract class Entity
{
    public string? Name { get; set; }
    public int HP { get; protected set; }

    public void DamageSelf()
    {
        if(HP > 15)
        {
            HP -= 15;
        }
    }
}
