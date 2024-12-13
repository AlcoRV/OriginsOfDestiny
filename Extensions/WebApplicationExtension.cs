using OriginsOfDestiny.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace OriginsOfDestiny.Extensions
{
    public static class WebApplicationExtension
    {
        public static void StartBot(this WebApplication app)
        {
            var cts = new CancellationTokenSource();
            var botClient = app.Services.GetRequiredService<ITelegramBotClient>();
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

            botClient.StartReceiving(
                async (botClient, update, cancellationToken) =>
                {
                    using var scope = scopeFactory.CreateScope();
                    var botHandler = scope.ServiceProvider.GetRequiredService<IComandHandler>();

                    if (update.Message != null && update.Message.Text != null)
                    {
                        // Обрабатываем текстовые команды
                        await botHandler.HandleMessageUpdateAsync(update.Message, cancellationToken);
                    }
                    if (update.CallbackQuery != null && update.CallbackQuery.Data != null)
                    {
                        await botHandler.HandleCallbackQueryUpdateAsync(update.CallbackQuery, cancellationToken);
                    }
                },
                (botClient, exception, cancellationToken) =>
                {
                    Console.WriteLine($"Произошла ошибка: {exception.Message}");
                    return Task.CompletedTask;
                },
                new ReceiverOptions { AllowedUpdates = { } },
                cancellationToken: cts.Token
            );

            app.Lifetime.ApplicationStopping.Register(() => cts.Cancel());
        }
    }
}
