using Microsoft.EntityFrameworkCore;
using OriginsOfDestiny.Data;
using OriginsOfDestiny.Extensions;
using OriginsOfDestiny.Handlers;
using OriginsOfDestiny.Models.Dialogs;
using OriginsOfDestiny.Models.Sessions;
using OriginsOfDestiny.Repositories;
using OriginsOfDestiny.Services;
using OriginsOfDestiny.Telegram;
using StackExchange.Redis;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

var telegramToken = builder.Configuration["Telegram:Token"];

// Add services to the container.
var botClient = new TelegramBotClient(telegramToken ?? throw new NullReferenceException("Telegram token not found!"));
builder.Services.AddSingleton<ITelegramBotClient>(botClient);
builder.Services.AddScoped<IComandHandler, ComandHandler>();

builder.Services.AddScoped<IRepository<Dialog>, DialogRepository>();
builder.Services.AddScoped<IRepository<UserSession>, SessionRepository>();

builder.Services.AddTransient<ISessionService, SessionService>();
builder.Services.AddTransient<IMenuService, MenuHandler>();
builder.Services.AddTransient<IRestartHandler, RestartHandler>();
builder.Services.AddTransient<IStartService, StartHandler>();
builder.Services.AddTransient<IDialogService, DialogService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();

var dbConnectionString = builder.Configuration["Db:ConnectionString"];
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(dbConnectionString ?? throw new NullReferenceException("Db connection string not found!")));

/*builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration["Redis:ConnectionString"];
    return ConnectionMultiplexer.Connect(configuration ?? throw new NullReferenceException("Redis configuration not found!"));
});*/

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
