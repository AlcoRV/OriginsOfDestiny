﻿using Telegram.Bot;

namespace OriginsOfDestiny.Common.Interfaces;

public interface ITelegramErrorHandler
{
    Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token);
}
