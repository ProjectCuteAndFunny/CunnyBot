using CunnyBot.Services;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, services) => {
        services.AddSingleton(_ => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.DirectMessages,
                // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                LogLevel = LogSeverity.Info | LogSeverity.Error,
                UseSystemClock = false
            }))
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddSingleton<InteractionHandler>()
            .AddSingleton(_ => new CommandService(new CommandServiceConfig
            {
                // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                LogLevel = LogSeverity.Info | LogSeverity.Error,
                DefaultRunMode = Discord.Commands.RunMode.Async
            }));
    })
    .Build();

using var serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;
var commands = provider.GetRequiredService<InteractionService>();
var client = provider.GetRequiredService<DiscordSocketClient>();
await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

client.Log += LogAsync;
commands.Log += LogAsync;

// Resister commands to a specific guild
// client.Ready += async () => await commands.RegisterCommandsToGuildAsync(Convert.ToUInt64(GetEnvironmentVariable("CUNNY_GUILD")));
// Registers commands globally
// client.Ready += async () => await commands.RegisterCommandsGloballyAsync();

await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("CUNNY_TOKEN"));
await client.StartAsync();
await Task.Delay(-1);

static async Task LogAsync(LogMessage message)
{
    var severity = message.Severity switch
    {
        LogSeverity.Critical => LogEventLevel.Fatal,
        LogSeverity.Error => LogEventLevel.Error,
        LogSeverity.Warning => LogEventLevel.Warning,
        LogSeverity.Info => LogEventLevel.Information,
        LogSeverity.Verbose => LogEventLevel.Verbose,
        LogSeverity.Debug => LogEventLevel.Debug,
        _ => LogEventLevel.Information
    };
    Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
    await Task.CompletedTask;
}