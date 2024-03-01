﻿using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.WaitingFor;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Models.Storage;

public class ClientData : IClientData
{
    public ITelegramBotClient BotClient { get; set; }
    public WaitingForBaseMessageHandler WaitingForMessage { get; set; }
    public IPlayerContext PlayerContext { get; set; }
    public ITimerHandler TimerHandler { get; set; }
    public IEnumerable<string> AvailablesCodes { get; set; }
}
