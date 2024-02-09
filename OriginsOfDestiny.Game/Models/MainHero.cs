namespace OriginsOfDestiny.Game.Models;

public class MainHero: Entity
{
    public MainHero() {
        HP = HP == 0 ? 100 : HP;
    }
}
