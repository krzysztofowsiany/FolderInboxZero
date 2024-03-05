using SQLite;
using static System.Environment;

namespace FolderInboxZero.Core.CurrentStorage;

public class CurrentStorageRepository
{
    private readonly SQLiteConnection _connection;

    public CurrentStorageRepository()
    {
        var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "current_folder_database.db3");
        _connection = new SQLiteConnection(dbPath, flags);
        _connection.CreateTable<StorageTable>();
    }

    public void AddStorages(string[] paths) => _connection.InsertAll(paths.Select(x => new StorageTable(x)));
}