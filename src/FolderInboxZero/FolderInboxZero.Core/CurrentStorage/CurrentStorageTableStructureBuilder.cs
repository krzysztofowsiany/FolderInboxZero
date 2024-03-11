namespace FolderInboxZero.Core.CurrentStorage;

public class CurrentStorageTableStructureBuilder
{
    private List<StorageTable> _storageItems = [];

    public void Clear() => _storageItems.Clear();

    public void GetStorageItems(string path, Guid parentId = default)
    {
        var directory = new StorageTable(path, parentId, Guid.NewGuid());
        _storageItems.Add(directory);

        var rootDicrectiories = Directory.GetDirectories(path);
        foreach (var rootDirectory in rootDicrectiories)
            GetStorageItems(rootDirectory, directory.Id);

        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            var fileItem = new StorageTable(file, parentId, Guid.NewGuid());
            _storageItems.Add(fileItem);
        }
    }

    public List<StorageTable> StorageItems => _storageItems;
}