using SQLite;

namespace FolderInboxZero.Core.StorageDatabase;

internal class StorageDatabaseRepository
{
    public StorageDatabaseRepository()
    {
        var filename = Path.Combine(Directory.GetCurrentDirectory(), ".database");
        var conn = new SQLiteConnection(filename);
    }
}
