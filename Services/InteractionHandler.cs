using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace CunnyBot.Services;
public class InteractionHandler
{
    private DiscordSocketClient Client { get; }
    private InteractionService Commands { get; }
    private IServiceProvider Services { get; }

    // Using constructor injection
    public InteractionHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
    {
        Client = client;
        Commands = commands;
        Services = services;
    }
    public async Task InitializeAsync()
    {
        // Add the public modules that inherit InteractionModuleBase<T> to the InteractionService
        await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);

        // Process the InteractionCreated payloads to execute Interactions commands
        Client.InteractionCreated += HandleInteraction;
        _= Client.SetGameAsync("Blue Archive", "https://www.twitch.tv/directory/game/Blue%20Archive", ActivityType.Competing);

        // Process the command execution results 
        Commands.SlashCommandExecuted += SlashCommandExecuted;
        Commands.ContextCommandExecuted += ContextCommandExecuted;
        Commands.ComponentCommandExecuted += ComponentCommandExecuted;
    }
    private static Task ComponentCommandExecuted(ComponentCommandInfo arg1, IInteractionContext arg2, IResult arg3) => Task.CompletedTask;

    private static Task ContextCommandExecuted(ContextCommandInfo arg1, IInteractionContext arg2, IResult arg3) => Task.CompletedTask;

    private static Task SlashCommandExecuted(SlashCommandInfo arg1, IInteractionContext arg2, IResult arg3) => Task.CompletedTask;

    private async Task HandleInteraction(SocketInteraction arg)
    {
        try {
            // Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules
            var ctx = new SocketInteractionContext(Client, arg);
            await Commands.ExecuteCommandAsync(ctx, Services);
        } catch (Exception ex) {
            Console.WriteLine(ex);

            // If a Slash Command execution fails it is most likely that the original interaction acknowledgement will persist.
            // It is a good idea to delete the original response, or at least let the user know that something went wrong during the command execution.
            if (arg.Type == InteractionType.ApplicationCommand)
                await arg.GetOriginalResponseAsync()
                    .ContinueWith(async msg => await msg.Result.DeleteAsync());
        }
    }
}
