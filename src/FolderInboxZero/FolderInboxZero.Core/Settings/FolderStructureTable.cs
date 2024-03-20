using SQLite;

public record FolderStructureTable()
{
    [PrimaryKey]
    [NotNull]
    public Guid Id { get; init; }
    public Guid ParentId { get; init; }
    public string Path { get; init; }
}