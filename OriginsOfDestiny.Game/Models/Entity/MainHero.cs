namespace OriginsOfDestiny.Game.Models.Entity;

public class MainHero : Entity
{
    public MainHero()
    {
        HP = HP == 0 ? 100 : HP;
    }
}
