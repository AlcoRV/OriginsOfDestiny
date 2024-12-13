using OriginsOfDestiny.Extensions;
using OriginsOfDestiny.Telegram;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

var telegramToken = builder.Configuration["Telegram:Token"];

// Add services to the container.
var botClient = new TelegramBotClient(telegramToken ?? throw new NullReferenceException("Telegram token not found!"));
builder.Services.AddSingleton<ITelegramBotClient>(botClient);
builder.Services.AddSingleton<IComandHandler, ComandHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.StartBot();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
