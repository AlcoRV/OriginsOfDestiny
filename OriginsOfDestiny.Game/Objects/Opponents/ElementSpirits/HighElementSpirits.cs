using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Models.Entity.Spirits;

namespace OriginsOfDestiny.Game.Objects.Opponents.ElementSpirits
{
    public class HighElementSpirits
    {
        public static readonly HighElementSpirit Simon = new()
        {
            Name = "SIMON",
            Gender = Gender.Man,
            Picture = "Characters/simon.jpg",
            Attitude = Attitude.Friendly
        };
    }
}
