using OriginsOfDestiny.Game.Enums;

namespace OriginsOfDestiny.Game.Models.Entity;

public class MainHero : Entity
{
    public MainHero(string name, Gender gender)
    {
        HP = HP == 0 ? 100 : HP;
        Name = name;
        Gender = gender;
    }
}
