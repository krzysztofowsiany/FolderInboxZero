using FolderInboxZero.Core.CurrentStorage;
using System.Collections.ObjectModel;
using UraniumUI;

namespace FolderInboxZero.Models;

public class TreeNode : UraniumBindableObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public bool IsDirectory { get; set; }
    public StorageStatus Status { get; set; }
    public ObservableCollection<TreeNode> Children { get; set; } = [];
}