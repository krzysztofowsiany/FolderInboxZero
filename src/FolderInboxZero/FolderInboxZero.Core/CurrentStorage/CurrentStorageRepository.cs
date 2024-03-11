using SQLite;

namespace FolderInboxZero.Core.CurrentStorage;

public class CurrentStorageRepository
{
    private SQLiteConnection? _connection = null;
    private string _path;
    const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;

    public CurrentStorageRepository() => _path = AppDomain.CurrentDomain.BaseDirectory;

    public void AddStorages(IEnumerable<StorageTable> storageItems) => _connection?.InsertAll(storageItems);


    public void Connect()
    {
        var dbPath = Path.Combine(_path, ".current_folder_database.db3");
        _connection = new SQLiteConnection(dbPath, Flags);
        _connection.CreateTable<StorageTable>();
    }

    public void SetCurrentDirectory(string path) => _path = path;
}