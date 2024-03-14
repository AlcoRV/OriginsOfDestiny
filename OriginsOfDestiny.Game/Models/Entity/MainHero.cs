using OriginsOfDestiny.Game.Enums;

namespace OriginsOfDestiny.Game.Models.Entity;

public class MainHero : Entity
{
    public override Gender Gender { get; set; } = Gender.Woman;
    public override int HP { get; set; } = 100;

}
