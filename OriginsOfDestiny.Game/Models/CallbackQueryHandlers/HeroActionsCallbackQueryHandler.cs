using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Constants;
using OriginsOfDestiny.Data.Models.Effects;
using OriginsOfDestiny.Data.Models.Items;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.Game.Constants;
using OriginsOfDestiny.Game.Extentions;
using OriginsOfDestiny.Game.Models.Actions;
using System.Text;
using Telegram.Bot.Types;
using Stream = OriginsOfDestiny.Data.Models.Items.InteractiveItems.Stream;

namespace OriginsOfDestiny.Models.CallbackQueryHandlers;

public class HeroActionsCallbackQueryHandler : ICallbackQueryHandler
{

    public async Task Handle(IGameData gameData, CallbackQuery callbackQuery)
    {
        var actionCode = callbackQuery.Data.Split('_')[0];

        if (actionCode.Equals(GConstants.Messages.HeroActions.LookAround)) {
            gameData.ClientData.PlayerContext.Hero.GetActions(gameData).LookAround();
        }
        else if (actionCode.Equals(DConstants.Messages.Items.Use))
        {
            var typeName = callbackQuery.Data.Split('_')[1];
            var objectId = callbackQuery.Data.Split('_')[2];

            if(gameData?.ClientData?.PlayerContext?.Area?.InteractiveItems == null) { return; }

            var item = gameData.ClientData.PlayerContext.Area.InteractiveItems
                .FirstOrDefault(it => it.GetType().Name.Equals(typeName)
                && it.Id == Guid.Parse(objectId));

            if(item == null) { return; }

            if(item is Stream stream)
            {
                await UseStream(gameData, stream);
            }
            else if(item is Hollow duplo)
            {
                await UseDuplo(gameData, duplo);
            }
        }
    }

    private static async Task UseStream(IGameData gameData, Stream stream)
    {
        string reply;
        var streamResourceHelper = new ResourceHelper<Stream>();

        var resultCode = "";
        Effect effect = null;

        if (new Random().NextDouble() > stream.Probability)
        {
            resultCode = DConstants.Messages.Effects.Positive;
            effect = stream.PositiveEffect;
        }
        else
        {
            resultCode = DConstants.Messages.Effects.Negative;
            effect = stream.NegativeEffect;
        }

        reply = string.Format(
            streamResourceHelper.GetValue(resultCode),
            Math.Abs(effect.Health)
            );

        gameData.ClientData.PlayerContext.Hero.HP += effect.Health;

        await gameData.ClientData.EditMainMessageAsync(
            caption: reply,
            replyMarkup: gameData.ClientData.MainMessage.ReplyMarkup
            );
    }

    private static async Task UseDuplo(IGameData gameData, Hollow duplo)
    {
        string reply;
        var duploResourceHelper = new ResourceHelper<Hollow>();

        if (new Random().NextDouble() > duplo.Probability)
        {
            if(duplo.Loot == null) { return; }
            if (!duplo.Loot.Any()) { reply = duploResourceHelper.GetValue(DConstants.Messages.Out.NotFound); }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine(duploResourceHelper.GetValue(DConstants.Messages.Out.Found));
                foreach ( var item in duplo.Loot ) { 
                    sb.AppendLine($"\t{item.Name}");

                    var heroInventory = gameData.ClientData.PlayerContext.Hero.Inventory as HashSet<IItem>;
                    heroInventory.Add(item);
                }
                (duplo.Loot as HashSet<IItem>).Clear();

                reply = sb.ToString();
            }
        }
        else
        {
            reply = string.Format(
                duploResourceHelper.GetValue(DConstants.Messages.Effects.Negative),
                Math.Abs(duplo.NegativeEffect.Health)
            );

            gameData.ClientData.PlayerContext.Hero.HP += duplo.NegativeEffect.Health;
        }

        await gameData.ClientData.EditMainMessageAsync(
            caption: reply,
            replyMarkup: gameData.ClientData.MainMessage.ReplyMarkup
            );
    }
}
