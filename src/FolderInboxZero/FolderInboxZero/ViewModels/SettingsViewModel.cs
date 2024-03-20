using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.Core.Settings;
using FolderInboxZero.ViewModels.Base;
using System.Collections.ObjectModel;

namespace FolderInboxZero.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    public string CurrentInboxFolder { get; set; } = "Not Selected";
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

    private void CreateBaseDestinationFolder(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            var newNode = new TreeNode() { Name = path };
            Nodes.Add(newNode);
            _structureRepository.AddStuctureFolder(newNode.Id, newNode.Name, default);
        }
    }

    private void LoadSettings()
    {
        _configurations = _structureRepository.LoadSettings();

        if (_configurations.ContainsKey(ConfigurationParams.CurrentInboxFolder))
            CurrentInboxFolder = _configurations[ConfigurationParams.CurrentInboxFolder];
    }

    [RelayCommand]
    async Task Add(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(NewFolderName))
        {
            var newNode = new TreeNode() { Name = NewFolderName };
            SelectedNode?.Children.Add(newNode);
            NewFolderName = string.Empty;

            _structureRepository.AddStuctureFolder(newNode.Id, newNode.Name, SelectedNode.Id);
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

        CreateBaseDestinationFolder(folderPickerResult.Folder.Path);
    }
}