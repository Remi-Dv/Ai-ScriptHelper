
public static class Program
{
    public static FilesStorage filesStorage;

    public static DiscordBot discordBot;
    public static OpenAi openAi;

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Ai-ScriptHelper, a program made by RemiDv");
        Console.WriteLine();

        filesStorage = new FilesStorage("Ai-ScriptHelper");
    }

    public static void StartBot(string _discordToken)
    {
        discordBot = new DiscordBot(_discordToken);
        discordBot.RunBotAsync().GetAwaiter().GetResult();
    }

    public static void StartOpenAi(string _openAiToken)
    {
        openAi = new OpenAi(_openAiToken);
    }
}