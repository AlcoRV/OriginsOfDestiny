using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;

namespace OriginsOfDestiny.Game.Models.Actions
{
    using Stream = DataObjects.Models.Items.InteractiveItems.Stream;

    public class StreamActions
    {
        private readonly IGameData _gameData;
        private readonly Stream _stream;
        private static readonly ResourceHelper<Stream> ResourceHelper = new();

        public StreamActions(IGameData gameData, Stream stream) {
            _gameData = gameData;
            _stream = stream;
        }

        public async Task Use()
        {
            var reply = "";
            int health;

            string resultCode;
            if (new Random().NextDouble() > _stream.Probability)
            {
                resultCode = Stream.Messages.HealTo;
                var startHealth = _gameData.ClientData.PlayerContext.Hero.HP;

                _stream.HealTo(_gameData.ClientData.PlayerContext.Hero);
                health = _gameData.ClientData.PlayerContext.Hero.HP - startHealth;
            }
            else
            {
                health = _stream.Damage.Value;

                if (_stream.DamageTo(_gameData.ClientData.PlayerContext.Hero))
                {
                    resultCode = Stream.Messages.Kill;
                }
                else
                {
                    resultCode = Stream.Messages.DamageTo;
                }
            }

            reply = string.Format(
                ResourceHelper.GetValue(resultCode),
            health
            );

            await _gameData.ClientData.EditMainMessageAsync(
            caption: reply.ToString(),
                replyMarkup: _gameData.ClientData.MainMessage.ReplyMarkup
                );
        }
    }
}
