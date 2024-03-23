using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.Core.CurrentStorage;
using FolderInboxZero.Core.Settings;
using FolderInboxZero.ViewModels.Base;
using System.Collections.ObjectModel;

namespace FolderInboxZero.ViewModels;

public partial class InboxViewModel : BaseViewModel
{
    public TreeNode SelectedNode { get; set; }
    public ObservableCollection<TreeNode> Nodes { get; set; } = [];

    private Dictionary<string, string> _configurations;
    private string CurrentInboxFolder;
    private readonly SettingsRepository _structureRepository;
    private readonly CurrentStorageRepository _currentStorageRepository;

    public InboxViewModel(SettingsRepository structureRepository, CurrentStorageRepository currentStorageRepository)
    {
        _structureRepository = structureRepository;
        _currentStorageRepository = currentStorageRepository;
    }

    private void LoadSettings()
    {
        _configurations = _structureRepository.LoadSettings();

        if (_configurations.ContainsKey(ConfigurationParams.CurrentInboxFolder))
            CurrentInboxFolder = _configurations[ConfigurationParams.CurrentInboxFolder];
    }

    [RelayCommand]
    async Task Appearing(CancellationToken cancellationToken)
    {
        LoadSettings();
        LoadInboxFolder();
    }

    private void FillTreeView(List<StorageTable> storageItems)
    {
        Nodes.Clear();

        foreach (var storage in storageItems)
        {
            var newNode = new TreeNode()
            {
                Name = storage.Path,
                Id = storage.Id,
                IsDirectory = storage.Type == StorageType.Folder
            };

            if (storage.ParentId == Guid.Empty)
            {
                Nodes.Add(newNode);
                continue;
            }

            var node = GetParent(Nodes, storage.ParentId);
            if (node != null)
                node.Children.Add(newNode);
        }
    }

    private TreeNode GetParent(ObservableCollection<TreeNode> nodes, Guid parentId)
    {
        var parent = nodes.FirstOrDefault(x => x.IsDirectory && x.Id.Equals(parentId));

        if (parent == null)
            foreach (var node in nodes.Where(x => x.IsDirectory))
                parent = GetParent(node.Children, parentId);

        return parent;
    }

    private void LoadInboxFolder()
    {
        var currentStorageTableBuilder = new CurrentStorageTableStructureBuilder();
        currentStorageTableBuilder.GetStorageItems(CurrentInboxFolder);

        _currentStorageRepository.SetCurrentDirectory(CurrentInboxFolder);
        _currentStorageRepository.Connect();
        _currentStorageRepository.AddStorages(currentStorageTableBuilder.StorageItems);

        FillTreeView(currentStorageTableBuilder.StorageItems);

        return;
    }

    [RelayCommand]
    async Task Back(CancellationToken cancellationToken)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}