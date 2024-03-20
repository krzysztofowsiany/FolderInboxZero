using System.Collections.ObjectModel;
using UraniumUI;

namespace FolderInboxZero.ViewModels;

public class TreeNode : UraniumBindableObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public ObservableCollection<TreeNode> Children { get; set; } = [];
}