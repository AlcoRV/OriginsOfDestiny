using OriginsOfDestiny.Common.Helpers;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Common.UI
{
    public static class UITools
    {
        public static InlineKeyboardButton GetButton<T>(string code, string separator = ":", params string[] additionalData)
        {
            var resourceHelper = new ResourceHelper<T>();

            var sb = new StringBuilder();
            sb.Append(code);

            if (additionalData.Any())
            {
                sb.Append(separator);
                sb.Append(string.Join(separator, additionalData));
            }

            return InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(code), sb.ToString());
        }
    }
}
