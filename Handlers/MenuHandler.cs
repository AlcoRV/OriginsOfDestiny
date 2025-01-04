using OriginsOfDestiny.Models.Characters;
using OriginsOfDestiny.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Handlers
{
    public class MenuHandler : IMenuService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IPlayerService _playerService;

        public MenuHandler(ITelegramBotClient botClient,
            IPlayerService playerService)
        {
            _botClient = botClient;
            _playerService = playerService;
        }

        public async Task HandleCallback(CallbackQuery query, CancellationToken token)
        {
            switch (query.Data)
            {
                case "menu_quests":
                    //await ShowQuests(query.From.Id, player, token);
                    break;
                case "menu_notes":
                    //await ShowNotes(query.From.Id, player, token);
                    break;
                case "menu_inventory":
                    //await ShowInventory(query.From.Id, player, token);
                    break;
                case "menu_character":
                    //await ShowCharacter(query.From.Id, player, token);
                    break;
                case "menu_explore":
                    //await ShowExploreOptions(query.From.Id, player, token);
                    break;
                default:
                    await ShowMainMenu(query.From.Id, token);
                    break;
            }
        }

        public async Task ShowMainMenu(long chatId, CancellationToken token)
        {
            var player = _playerService.GetByTelegramId(chatId);

            var healthMessage = $"Ваше здоровье: {player.Health}/{player.MaxHealth}";

            await _botClient.SendMessage(
                chatId,
                $"{healthMessage}\n\nВыберите действие:",
                replyMarkup: GetMainMenu(),
                cancellationToken: token
            );
        }

        private InlineKeyboardMarkup GetMainMenu()
        {
            var buttons = new[]
            {
                InlineKeyboardButton.WithCallbackData("Задания", "menu_quests"),
                InlineKeyboardButton.WithCallbackData("Записки", "menu_notes"),
                InlineKeyboardButton.WithCallbackData("Инвентарь", "menu_inventory"),
                InlineKeyboardButton.WithCallbackData("О персонаже", "menu_character"),
        };
            return new InlineKeyboardMarkup(new[] { 
                buttons, 
                [InlineKeyboardButton.WithCallbackData("Оглядеться", "menu_explore")] 
            });
        }

    }
}
