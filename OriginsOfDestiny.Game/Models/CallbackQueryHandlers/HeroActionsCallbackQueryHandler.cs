using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Constants;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.Game.Constants;
using OriginsOfDestiny.Game.Extentions;
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
            else if(item is Hollow hollow)
            {
                await UseHollow(gameData, hollow);
            }
        }
    }

    private static async Task UseStream(IGameData gameData, Stream stream)
    {
        var reply = "";
        var streamResourceHelper = new ResourceHelper<Stream>();
        int health;

        string resultCode;
        if (new Random().NextDouble() > stream.Probability)
        {
            resultCode = Stream.Messages.HealTo; 
            var startHealth = gameData.ClientData.PlayerContext.Hero.HP;

            stream.HealTo(gameData.ClientData.PlayerContext.Hero);
            health = gameData.ClientData.PlayerContext.Hero.HP - startHealth;
        }
        else
        {
            health = stream.Damage.Value;

            if (stream.DamageTo(gameData.ClientData.PlayerContext.Hero))
            {
                resultCode = Stream.Messages.Kill;
            }
            else
            {
                resultCode = Stream.Messages.DamageTo;
            }
        }

        reply = string.Format(
            streamResourceHelper.GetValue(resultCode),
            health
            );

        await gameData.ClientData.EditMainMessageAsync(
            caption: reply.ToString(),
            replyMarkup: gameData.ClientData.MainMessage.ReplyMarkup
            );
    }

    private static async Task UseHollow(IGameData gameData, Hollow hollow)
    {
        string reply;
        var hollowResourceHelper = new ResourceHelper<Hollow>();

        if (new Random().NextDouble() > hollow.Probability)
        {
            if(hollow.Loot == null) { return; }
            if (!hollow.Loot.Any()) { reply = hollowResourceHelper.GetValue(DConstants.Messages.Out.NotFound); }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine(hollowResourceHelper.GetValue(DConstants.Messages.Out.Found));
                foreach ( var item in hollow.Loot ) { 
                    sb.AppendLine($"🔹 {item.Name}");

                    var heroInventory = gameData.ClientData.PlayerContext.Hero.Inventory as HashSet<IItem>;
                    heroInventory.Add(item);
                }
                (hollow.Loot as HashSet<IItem>).Clear();

                reply = sb.ToString();
            }
        }
        else
        {
            var damage = hollow.Damage.Value;
            if (hollow.DamageTo(gameData.ClientData.PlayerContext.Hero))
            {
                reply = hollowResourceHelper.GetValue(Hollow.Messages.Kill);
            }
            else
            {
                reply = string.Format(
                    hollowResourceHelper.GetValue(Hollow.Messages.DamageTo),
                    damage
                );
            }
        }

        await gameData.ClientData.EditMainMessageAsync(
            caption: reply,
            replyMarkup: gameData.ClientData.MainMessage.ReplyMarkup
            );
    }
}
