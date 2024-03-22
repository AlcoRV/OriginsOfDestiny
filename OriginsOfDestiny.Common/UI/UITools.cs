using OriginsOfDestiny.Common.Helpers;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Common.UI
{
    public static class UITools
    {
        public static InlineKeyboardButton GetButton<T>(string code)
        {
            var resourceHelper = new ResourceHelper<T>();

            return InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(code), code);
        }
    }
}
