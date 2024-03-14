using OriginsOfDestiny.Common.Models.Storage;
using OriginsOfDestiny.Data.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class HeroActions
    {
        private readonly GameData _gameData;
        private readonly Hero _hero;

        public HeroActions(GameData gameData, Hero hero)
        {
            _gameData = gameData;
            _hero = hero;
        }

        public void LookAround()
        {

        }
    }
}
