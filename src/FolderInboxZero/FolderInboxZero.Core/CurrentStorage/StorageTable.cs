namespace FolderInboxZero.Core.CurrentStorage;

public record StorageTable (string Path, Guid ParentId, Guid Id, StorageType Type, StorageStatus Status = StorageStatus.ToDo);