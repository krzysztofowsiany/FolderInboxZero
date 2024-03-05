using SQLite;

namespace FolderInboxZero.Core.Structure;

public class StructureRepository
{
    private readonly SQLiteConnection _connection;

    public StructureRepository()
    {
        var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "structure.db3");

        _connection = new SQLiteConnection(dbPath, flags);
    }
}
