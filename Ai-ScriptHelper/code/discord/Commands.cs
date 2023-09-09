using Discord.Commands;

public class Commands : ModuleBase<SocketCommandContext>
{
    [Command("help")]
    public async Task HelpAsync()
    {
        await ReplyAsync("aa");
    }
}
