using SQLite;

namespace FolderInboxZero.Core.Settings;

public class SettingsRepository
{
    private readonly SQLiteConnection _connection;

    public SettingsRepository()
    {
        var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "settings.db3");

        _connection = new SQLiteConnection(dbPath, flags);
        _connection.CreateTable<ConfigurationTable>();
        _connection.CreateTable<FolderStructureTable>();
    }

    public void SaveInboxFolder(string currentInboxFolder) =>
        AddConfiguration(ConfigurationParams.CurrentInboxFolder, currentInboxFolder);

    private void AddConfiguration(string key, string value)
    {
        _connection?.InsertOrReplace(new ConfigurationTable()
        {
            Key = key,
            Value = value
        });
    }

    public Dictionary<string, string> LoadSettings()
    {
        var configurations = _connection?.Query<ConfigurationTable>("SELECT * FROM ConfigurationTable");

        return configurations
            .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
            .ToDictionary();
    }

    public void AddStuctureFolder(Guid id, string path, Guid parentId)
    {
        _connection?.InsertOrReplace(new FolderStructureTable()
        {
            Id = id,
            ParentId = parentId,
            Path = path,
        });
    }

    public IEnumerable<IGrouping<Guid, FolderStructureTable>> LoadStructure()
    {
        var folders = _connection?.Query<FolderStructureTable>("SELECT * FROM FolderStructureTable");

        return folders.GroupBy(x => x.ParentId);
    }


    public void RemoveStructureByIds(List<Guid> ids)
    {
        var stringIds = ids.Select(x => $"'{x}'");

        var joinedIds = string.Join(",", stringIds);
        _connection?.Execute($"DELETE FROM FolderStructureTable WHERE id IN({joinedIds})");
    }
}
