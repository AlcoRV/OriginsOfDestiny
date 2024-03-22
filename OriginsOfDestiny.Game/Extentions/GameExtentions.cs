using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.DataObjects.Models.Entity;
using OriginsOfDestiny.Game.Models.Actions;

namespace OriginsOfDestiny.Game.Extentions;

public static class GameExtentions
{
    public static HeroActions GetActions(this Hero hero, IGameData gameData) => new(gameData);
}
