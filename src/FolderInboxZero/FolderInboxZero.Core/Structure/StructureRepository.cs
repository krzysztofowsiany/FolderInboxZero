using SQLite;

namespace FolderInboxZero.Core.Structure;

internal class StructureRepository
{
    public StructureRepository()
    {
        var filename = Path.Combine(FileSystem.AppDataDirectory, "structure.database");
        var conn = new SQLiteConnection(filename);
    }
}
