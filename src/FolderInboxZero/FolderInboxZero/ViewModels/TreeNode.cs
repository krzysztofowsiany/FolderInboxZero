using System.Collections.ObjectModel;
using UraniumUI;

namespace FolderInboxZero.ViewModels;

public class TreeNode : UraniumBindableObject
{
    public required string Name { get; set; }
    public ObservableCollection<TreeNode> Children { get; set; } = [];
}