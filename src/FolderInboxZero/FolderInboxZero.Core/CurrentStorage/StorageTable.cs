namespace FolderInboxZero.Core.CurrentStorage;

internal record StorageTable (string Path, StorageStatus Status = StorageStatus.ToDo);