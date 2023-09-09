using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

public class DiscordBot
{
    private string discordToken;
    private DiscordSocketClient client;

    private CommandService commands;

    public DiscordBot(string _discordToken) 
    {
        discordToken = _discordToken;
    }

    public async Task RunBotAsync()
    {
        DiscordSocketConfig config = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.All
        };

        client = new DiscordSocketClient(config);

        commands = new CommandService();

        client.Ready += () => {
            Console.WriteLine("The bot started");
            return Task.CompletedTask;
        };

        await InstallCommandsAsync();

        await client.LoginAsync(TokenType.Bot, discordToken);
        await client.StartAsync();
        await Task.Delay(-1);
    }

    private async Task InstallCommandsAsync()
    {
        client.MessageReceived += HandleCommandAsync;
        await commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
    }

    private async Task HandleCommandAsync(SocketMessage pMessage)
    {
        var message = pMessage as SocketUserMessage;

        if (message == null)
        {
            return;
        }

        int argPos = 0;

        if (!message.HasCharPrefix('!', ref argPos) || message.Author.IsBot)
        {
            return;
        }

        var context = new SocketCommandContext(client, message);

        var result = await commands.ExecuteAsync(context, argPos, null);

        if (!result.IsSuccess)
        {
            if (result.Error == CommandError.UnknownCommand)
            {
                await context.Channel.SendMessageAsync("Unknown Command !Help for more informations");
            }
            else
            {
                await context.Channel.SendMessageAsync("Error :" + result.ErrorReason);
            }
        }
    }
}