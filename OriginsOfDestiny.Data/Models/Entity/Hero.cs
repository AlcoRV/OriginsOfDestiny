using OriginsOfDestiny.Data.Enums;

namespace OriginsOfDestiny.Data.Models.Entity;

public class Hero : Entity
{
    public override Gender Gender { get; set; } = Gender.Woman;
    public override int HP { get; set; } = 100;

}
