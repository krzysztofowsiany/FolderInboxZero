using FolderInboxZero.Core.Settings;
using FolderInboxZero.ViewModels;
using System.Collections.ObjectModel;

namespace FolderInboxZero.Services;

public class LoadFolderStructureService
{
    private readonly FolderStructureService _folderStructureService;

    public LoadFolderStructureService(FolderStructureService folderStructureService)
    {
        _folderStructureService = folderStructureService;
    }

    public void LoadStructure(ObservableCollection<TreeNode> nodes)
    {
        nodes.Clear();
        _folderStructureService.LoadStructure();

        var baseFolders = _folderStructureService.GetBaseFolders();
        if (baseFolders is null) return;

        foreach (var baseFolder in baseFolders)
        {
            var newNode = new TreeNode()
            {
                Name = baseFolder.Path,
                Id = baseFolder.Id,
            };

            AddSubFolders(newNode, baseFolder.Id);

            nodes.Add(newNode);
        }
    }

    private void AddSubFolders(TreeNode node, Guid parentId)
    {
        var subFolders = _folderStructureService.GetSubFoldersForParentId(parentId);
        if (subFolders is null) return;

        foreach (var subFolder in subFolders)
        {
            var newNode = new TreeNode()
            {
                Name = subFolder.Path,
                Id = subFolder.Id,
            };

            AddSubFolders(newNode, subFolder.Id);

            node.Children.Add(newNode);
        }
    }
}
