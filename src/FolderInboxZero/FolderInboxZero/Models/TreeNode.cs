using FolderInboxZero.Core.CurrentStorage;
using System.Collections.ObjectModel;
using UraniumUI;

namespace FolderInboxZero.Models;

public class TreeNode : UraniumBindableObject
{
    private StorageStatus _storageStatus;
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public bool IsDirectory { get; set; }
    public StorageStatus Status
    {
        get => _storageStatus;
        set => SetProperty(ref _storageStatus, value);
    }

    public ObservableCollection<TreeNode> Children { get; set; } = [];
}