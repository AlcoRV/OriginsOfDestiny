using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Game.Constants;
using OriginsOfDestiny.Game.Extentions;
using OriginsOfDestiny.Game.Models.Actions;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Models.CallbackQueryHandlers;

public class HeroActionsCallbackQueryHandler : ICallbackQueryHandler
{
    private readonly ResourceHelper<HeroActions> ResourceHelper = new();
    public async Task Handle(IGameData gameData, CallbackQuery callbackQuery)
    {
        if (callbackQuery.Data.Equals(Constants.HeroActions.LookAround)) {
            gameData.ClientData.PlayerContext.Hero.GetActions(gameData).LookAround();
        }
    }
}
