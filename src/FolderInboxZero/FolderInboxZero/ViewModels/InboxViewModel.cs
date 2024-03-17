using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.Core.Settings;
using FolderInboxZero.ViewModels.Base;

namespace FolderInboxZero.ViewModels;

public partial class InboxViewModel : BaseViewModel
{
    private Dictionary<string, string> _configurations;

    private readonly SettingsRepository _structureRepository;

    public InboxViewModel(SettingsRepository structureRepository)
    {
        _structureRepository = structureRepository;

        LoadSettings();
    }

    private void LoadSettings()
    {
        _configurations = _structureRepository.LoadSettings();

    }

    [RelayCommand]
    async Task Back(CancellationToken cancellationToken)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}