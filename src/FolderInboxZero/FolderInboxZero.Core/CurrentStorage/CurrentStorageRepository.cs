using SQLite;

namespace FolderInboxZero.Core.CurrentStorage;

internal class CurrentStorageRepository
{
    public CurrentStorageRepository()
    {
        var filename = Path.Combine(Directory.GetCurrentDirectory(), ".current_folder_database");
        var conn = new SQLiteConnection(filename);
    }
}
