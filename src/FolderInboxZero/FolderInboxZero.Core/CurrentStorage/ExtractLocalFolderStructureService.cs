namespace FolderInboxZero.Core.CurrentStorage;

public class ExtractLocalFolderStructureService
{
    public string[] GetStorageItems(string path) => Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
}
