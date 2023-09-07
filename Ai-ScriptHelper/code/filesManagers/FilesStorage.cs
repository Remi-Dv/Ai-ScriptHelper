

public class FilesStorage
{
    private string projectName;
    private string directoryPath;

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
    }
}