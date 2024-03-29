using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Constants;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using System.Text;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class HollowActions
    {
        private readonly IGameData _gameData;
        private readonly Hollow _hollow;
        private static readonly ResourceHelper<Hollow> ResourceHelper = new();

        public HollowActions(IGameData gameData, Hollow hollow)
        {
            _gameData = gameData;
            _hollow = hollow;
        }

        public async Task Use()
        {
            string reply;
            var hollowResourceHelper = new ResourceHelper<Hollow>();

            if (new Random().NextDouble() > _hollow.Probability)
            {
                if (_hollow.Loot == null) { return; }
                if (!_hollow.Loot.Any()) { reply = hollowResourceHelper.GetValue(DConstants.Messages.Out.NotFound); }
                else
                {
                    var sb = new StringBuilder();
                    sb.AppendLine(hollowResourceHelper.GetValue(DConstants.Messages.Out.Found));
                    foreach (var item in _hollow.Loot)
                    {
                        sb.AppendLine($"🔹 {item.Name}");

                        var heroInventory = _gameData.ClientData.PlayerContext.Hero.Inventory as HashSet<IItem>;
                        heroInventory.Add(item);
                    }
                    (_hollow.Loot as HashSet<IItem>).Clear();

                    reply = sb.ToString();
                }
            }
            else
            {
                var damage = _hollow.Damage.Value;
                if (_hollow.DamageTo(_gameData.ClientData.PlayerContext.Hero))
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

            await _gameData.ClientData.EditMainMessageAsync(
                caption: reply,
                replyMarkup: _gameData.ClientData.MainMessage.ReplyMarkup
                );
        }
    }
}
