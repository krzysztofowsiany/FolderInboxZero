
using SQLite;

public record ConfigurationTable()
{
    [PrimaryKey]
    [NotNull]
    public string Key { get; init; }
    public string Value { get; init; }
}