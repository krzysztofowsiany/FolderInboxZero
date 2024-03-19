using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.Core.Settings;
using FolderInboxZero.ViewModels.Base;
using System.Collections.ObjectModel;

namespace FolderInboxZero.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    public string CurrentInboxFolder { get; set; } = "Not Selected";
    public string DestinationBaseFolder { get; set; } = "Not Selected";
    
    public string NewFolderName { get; set; }
    public TreeNode SelectedNode { get; set; }
    public ObservableCollection<TreeNode> Nodes { get; set; } = [];
    
    private Dictionary<string, string> _configurations;

    readonly IFolderPicker _folderPicker;
    private readonly SettingsRepository _structureRepository;

    public SettingsViewModel(IFolderPicker folderPicker, SettingsRepository structureRepository)
    {
        _folderPicker = folderPicker;
        _structureRepository = structureRepository;

        LoadSettings();
    }

    private void CreateRootNode()
    {
        if (!string.IsNullOrEmpty(DestinationBaseFolder))
            Nodes.Add(new TreeNode() { Name = DestinationBaseFolder });
    }

    private void LoadSettings()
    {
        _configurations = _structureRepository.LoadSettings();

        if (_configurations.ContainsKey(ConfigurationParams.CurrentInboxFolder))
            CurrentInboxFolder = _configurations[ConfigurationParams.CurrentInboxFolder];

        if (_configurations.ContainsKey(ConfigurationParams.DestinationBaseFolder))
            DestinationBaseFolder = _configurations[ConfigurationParams.DestinationBaseFolder];
    }

    [RelayCommand]
    async Task Add(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(NewFolderName))
        {
            SelectedNode?.Children.Add(new TreeNode() { Name = NewFolderName });
            NewFolderName = string.Empty;
        }
    }

    [RelayCommand]
    async Task Delete(CancellationToken cancellationToken)
    {
        //  if (SelectedNode!=null)

        //  SelectedNode?.Children.Add(new TreeNode(NewFolderName));
    }

    [RelayCommand]
    async Task Back(CancellationToken cancellationToken)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    [RelayCommand]
    async Task PickInboxFolder(CancellationToken cancellationToken)
    {
        var folderPickerResult = await _folderPicker.PickAsync(cancellationToken);
        if (!folderPickerResult.IsSuccessful) return;

        CurrentInboxFolder = folderPickerResult.Folder.Path;
        _structureRepository.SaveInboxFolder(CurrentInboxFolder);
    }

    [RelayCommand]
    async Task PickDestinationBaseFolder(CancellationToken cancellationToken)
    {
        var folderPickerResult = await _folderPicker.PickAsync(cancellationToken);
        if (!folderPickerResult.IsSuccessful) return;

        DestinationBaseFolder = folderPickerResult.Folder.Path;
        _structureRepository.SaveDestinationBaseFolder(DestinationBaseFolder);

        CreateRootNode();
    }
}