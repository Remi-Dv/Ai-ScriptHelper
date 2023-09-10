using Discord.Commands;

public class Commands : ModuleBase<SocketCommandContext>
{
    [Command("help")]
    public async Task HelpAsync()
    {
        await ReplyAsync("Availables commands:\n" +
            "- !help");
    }

    [Command("test")]
    public async Task TestAsync(string prompt)
    {
        
    }
}
