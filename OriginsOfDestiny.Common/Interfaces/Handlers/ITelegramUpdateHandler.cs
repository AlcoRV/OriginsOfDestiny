﻿using Telegram.Bot.Types;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Interfaces.Handlers;

public interface ITelegramUpdateHandler
{
    Task Update(ITelegramBotClient botClient, Update update, CancellationToken token);
}
