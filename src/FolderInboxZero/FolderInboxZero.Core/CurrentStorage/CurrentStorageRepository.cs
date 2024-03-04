using SQLite;

namespace FolderInboxZero.Core.CurrentStorage;

public class CurrentStorageRepository
{
    private readonly SQLiteConnection _connection;

    public CurrentStorageRepository()
    {
        var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;

        _connection = new SQLiteConnection("current_folder_database", flags);
        _connection.CreateTable<StorageTable>();
    }

    public void AddStorages(string[] paths) => _connection.InsertAll(paths.Select(x => new StorageTable(x)));
}