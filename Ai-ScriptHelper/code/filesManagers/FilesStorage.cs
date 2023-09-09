

public class FilesStorage
{
    private string projectName;
    private string directoryPath;

    private string settingsPath;
    private string generatedFilesDirectoryPath;

    public FilesStorage(string _projectName)
    {
        projectName = _projectName;
        directoryPath = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.ApplicationData), "RemiDv", projectName);
        CreateDirectories();
        SetupApplication();
    }

    private void CreateDirectories()
    {
        Directory.CreateDirectory(directoryPath);

        generatedFilesDirectoryPath = Path.Combine(directoryPath, "generatedFiles");

        settingsPath = Path.Combine(directoryPath, "settings.bin");
    }

    public void SetupApplication()
    {
        if (!File.Exists(settingsPath))
        {
            CreateSettingFile();
        }

        Start(LoadSettings());
    }

    private void CreateSettingFile()
    {
        TokensStruct tokens = new TokensStruct();

        Console.Write("Discord token: ");
        tokens.discordToken = Console.ReadLine();
        Console.Write("OpenAi token: ");
        tokens.openAiToken = Console.ReadLine();

        byte[] binaryTokens = SaveToFile.ConvertObjectToBytes(tokens);

        SaveToFile.SaveFile(binaryTokens, settingsPath);
    }

    private TokensStruct LoadSettings()
    {
        byte[] binaryTokens = SaveToFile.LoadFile(settingsPath); //get the file

        TokensStruct tokens = (TokensStruct)SaveToFile.ConvertBytesToObject(binaryTokens); //get the object

        return tokens;
    }

    private async void Start(TokensStruct _tokens)
    {
        Program.StartBot(_tokens.discordToken);
    }
}