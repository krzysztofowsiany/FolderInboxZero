namespace FolderInboxZero.Core.CurrentStorage;

public record StorageTable (string Path, Guid ParentId, Guid Id, StorageStatus Status = StorageStatus.ToDo);