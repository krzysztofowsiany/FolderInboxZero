using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.ViewModels.Base;

namespace FolderInboxZero.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    public SettingsViewModel()
    {
    }


    [RelayCommand]
    async Task Back(CancellationToken cancellationToken)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}