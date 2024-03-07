﻿using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Models.WaitingFor;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IClientData
{
    public ITelegramBotClient BotClient { get; set; }
    public WaitingForBaseMessageHandler WaitingForMessage { get; set; }
    public IPlayerContext PlayerContext { get; set; }
    public ITimerHandler TimerHandler { get; set; }
    public IEnumerable<string> AvailablesCodes { get; set; }
    public IMessageHandler DefaultMessageHandler { get; set; }
    public string LastCode { get; set; }
    public IEnumerable<string> RiddenMessagesCodes { get; set; }
}
