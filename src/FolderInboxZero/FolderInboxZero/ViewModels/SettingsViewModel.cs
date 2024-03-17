using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.Core.Settings;
using FolderInboxZero.ViewModels.Base;
using System.Collections.ObjectModel;

namespace FolderInboxZero.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{

    [ObservableProperty]
    public string currentInboxFolder = "Not Selected";

    public TreeNode SelectedNode { get; set; }
    public ObservableCollection<TreeNode> Nodes { get; set; } = new();

    private Dictionary<string, string> _configurations;

    readonly IFolderPicker _folderPicker;
    private readonly SettingsRepository _structureRepository;

    public SettingsViewModel(IFolderPicker folderPicker, SettingsRepository structureRepository)
    {
        _folderPicker = folderPicker;
        _structureRepository = structureRepository;

        LoadSettings();
        Nodes.Add(new TreeNode("A")
        {
            Children =
            {
                new TreeNode("A.1"),
                new TreeNode("A.2"),
            }
        });
    }

    private void LoadSettings()
    {
        _configurations = _structureRepository.LoadSettings();

        if (_configurations.ContainsKey(ConfigurationParams.CurrentInboxFolder))
            currentInboxFolder = _configurations[ConfigurationParams.CurrentInboxFolder];
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
}