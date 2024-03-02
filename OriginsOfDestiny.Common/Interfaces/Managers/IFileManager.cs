namespace OriginsOfDestiny.Common.Interfaces.Managers;

public interface IFileManager
{
    public FileStream GetFileStream(string fileName);
}
