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
            var botHandler = app.Services.GetRequiredService<IComandHandler>();

            botClient.StartReceiving(
                async (botClient, update, cancellationToken) =>
                {
                    if (update.Message != null && update.Message.Text != null)
                    {
                        // Обрабатываем текстовые команды
                        await botHandler.HandleUpdateAsync(update.Message, cancellationToken);
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
