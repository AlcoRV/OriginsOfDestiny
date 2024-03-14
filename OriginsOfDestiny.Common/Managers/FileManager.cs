using OriginsOfDestiny.Common.Interfaces.Managers;

namespace OriginsOfDestiny.Common.Managers;

public class FileManager : IFileManager
{
    public FileStream GetFileStream(string fileName)
    {
        var parentDirectory = Directory.GetParent(AppContext.BaseDirectory)!.FullName;

        var fileStream = File.Open($"{parentDirectory}/wwwroot/{fileName}", FileMode.Open);

        return fileStream;
    }
}
