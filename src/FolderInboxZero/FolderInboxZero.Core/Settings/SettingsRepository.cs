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

    public void SaveInboxFolder(string currentInboxFolder)
    {
        _connection?.InsertOrReplace(new ConfigurationTable()
        {
            Key = ConfigurationParams.CurrentInboxFolder,
            Value = currentInboxFolder
        });
    }

    public Dictionary<string, string> LoadSettings()
    {
        var configurations = _connection?.Query<ConfigurationTable>("SELECT * FROM ConfigurationTable");

        return configurations
            .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
            .ToDictionary();
    }
}
