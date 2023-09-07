

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
    }

    private void CreateDirectories()
    {
        Directory.CreateDirectory(directoryPath);

        generatedFilesDirectoryPath = Path.Combine(directoryPath, "generatedFiles");

        settingsPath = Path.Combine(directoryPath, "settings.crypt");
    }
}