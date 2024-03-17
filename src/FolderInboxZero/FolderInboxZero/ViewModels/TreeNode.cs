using System.Collections.ObjectModel;

namespace FolderInboxZero.ViewModels;

public class TreeNode
{
    public TreeNode()
    {
    }
    
    public TreeNode(string name)
    {
        Name = name;
    }

    public virtual string Name { get; set; }
    public virtual IList<TreeNode> Children { get; set; } = new ObservableCollection<TreeNode>();
}