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

    public void SaveDestinationBaseFolder(string destinationBaseFolder) =>
        AddConfiguration(ConfigurationParams.DestinationBaseFolder, destinationBaseFolder);

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
}
