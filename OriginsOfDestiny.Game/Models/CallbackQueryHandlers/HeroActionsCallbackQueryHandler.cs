using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Constants;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.Game.Constants;
using OriginsOfDestiny.Game.Extentions;
using OriginsOfDestiny.Game.Models.Actions;
using System.Text;
using Telegram.Bot.Types;
using Stream = OriginsOfDestiny.DataObjects.Models.Items.InteractiveItems.Stream;

namespace OriginsOfDestiny.Models.CallbackQueryHandlers;

public class HeroActionsCallbackQueryHandler : ICallbackQueryHandler
{

    public async Task Handle(IGameData gameData, CallbackQuery callbackQuery)
    {
        var data = callbackQuery.Data.Split('_');
        var actionCode = data[0];

        if (actionCode.Equals(HeroActions.Constants.LookAround)) {
            await gameData.ClientData.PlayerContext.Hero.GetActions(gameData).LookAround();
        }
        else if (actionCode.Equals(HeroActions.Constants.AboutPlayer))
        {
            await gameData.ClientData.PlayerContext.Hero.GetActions(gameData).AboutPlayer();
        }
        else if (actionCode.Equals(HeroActions.Constants.Notes))
        {
            await gameData.ClientData.PlayerContext.Hero.GetActions(gameData).Notes(callbackQuery.Data);
        }
        else if (actionCode.Equals(IInteractiveItem.Messages.Interact))
        {
            var typeName = data[1];
            var objectId = data[2];

            if(gameData?.ClientData?.PlayerContext?.Area?.InteractiveItems == null) { return; }

            var item = gameData.ClientData.PlayerContext.Area.InteractiveItems
                .FirstOrDefault(it => it.GetType().Name.Equals(typeName)
                && it.Id == Guid.Parse(objectId));

            if(item == null) { return; }

            if(item is Stream stream)
            {
                await new StreamActions(gameData, stream).Use();
            }
            else if(item is Hollow hollow)
            {
                await new HollowActions(gameData, hollow).Use();
            }
        }
    }

}
