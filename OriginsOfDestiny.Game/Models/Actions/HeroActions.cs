using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Models.Entity;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class HeroActions
    {
        private readonly IGameData _gameData;

        public HeroActions(IGameData gameData)
        {
            _gameData = gameData;
        }

        public void LookAround()
        {

        }
    }
}
