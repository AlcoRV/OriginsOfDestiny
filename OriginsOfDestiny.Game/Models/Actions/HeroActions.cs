using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Models.Entity;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class HeroActions
    {
        private readonly IGameData _gameData;
        private readonly Hero _hero;

        public HeroActions(IGameData gameData, Hero hero)
        {
            _gameData = gameData;
            _hero = hero;
        }

        public void LookAround()
        {

        }
    }
}
