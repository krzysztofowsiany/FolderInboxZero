using SQLite;

namespace FolderInboxZero.Core.Settings;

public class SettingsRepository
{
    private const  SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
    private readonly string _dbPath;

    public SettingsRepository()
    {
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, "settings.db3");
        CreateTables();
    }

    private void CreateTables()
    {
        using var connection = new SQLiteConnection(_dbPath, Flags);
        connection.CreateTable<ConfigurationTable>();
        connection.CreateTable<FolderStructureTable>();
        connection?.Close();
    }

    public void SaveInboxFolder(string currentInboxFolder) =>
        AddConfiguration(ConfigurationParams.CurrentInboxFolder, currentInboxFolder);

    private void AddConfiguration(string key, string value)
    {
        using var connection = new SQLiteConnection(_dbPath, Flags);
        connection?.InsertOrReplace(new ConfigurationTable()
        {
            Key = key,
            Value = value
        });
        connection?.Close();
    }

    public Dictionary<string, string> LoadSettings()
    {
        using var connection = new SQLiteConnection(_dbPath, Flags);
        var configurations = connection?.Query<ConfigurationTable>("SELECT * FROM ConfigurationTable");
        connection?.Close();

        return configurations
            .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
            .ToDictionary();
    }

    public void AddStuctureFolder(Guid id, string path, Guid parentId)
    {
        using var connection = new SQLiteConnection(_dbPath, Flags);
        connection?.InsertOrReplace(new FolderStructureTable()
        {
            Id = id,
            ParentId = parentId,
            Path = path,
        });
        connection?.Close();
    }

    public IEnumerable<IGrouping<Guid, FolderStructureTable>> LoadStructure()
    {
        using var connection = new SQLiteConnection(_dbPath, Flags);
        var folders = connection?.Query<FolderStructureTable>("SELECT * FROM FolderStructureTable");
        connection?.Close();

        return folders.GroupBy(x => x.ParentId);
    }


    public void RemoveStructureByIds(List<Guid> ids)
    {
        var joinedIds = string.Join(",", ids.Select(x => $"'{x}'"));
        var query = $"DELETE FROM FolderStructureTable WHERE id IN({joinedIds})";
        
        using var connection = new SQLiteConnection(_dbPath, Flags);
        connection?.Execute(query);
        connection?.Close();
    }
}
