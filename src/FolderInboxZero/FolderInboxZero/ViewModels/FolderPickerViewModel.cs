using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.Core.CurrentStorage;
using FolderInboxZero.ViewModels.Base;

namespace FolderInboxZero.ViewModels;

public partial class FolderPickerViewModel : BaseViewModel
{
    readonly IFolderPicker _folderPicker;
    private readonly CurrentStorageRepository _currentStorageRepository;

    public FolderPickerViewModel(IFolderPicker folderPicker, CurrentStorageRepository currentStorageRepository)
    {
        _folderPicker = folderPicker;

        _currentStorageRepository = currentStorageRepository;

        var extractLocalFolderStructureService = new Core.CurrentStorage.ExtractLocalFolderStructureService();
        var items = extractLocalFolderStructureService.GetStorageItems(AppDomain.CurrentDomain.BaseDirectory);
        _currentStorageRepository.AddStorages(items);
        _currentStorageRepository = currentStorageRepository;
    }

    [RelayCommand]
    async Task PickFolder(CancellationToken cancellationToken)
    {
        var folderPickerResult = await _folderPicker.PickAsync(cancellationToken);
        if (folderPickerResult.IsSuccessful)
        {
            await Toast.Make($"Folder picked: Name - {folderPickerResult.Folder.Name}, Path - {folderPickerResult.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        }
        else
        {
            await Toast.Make($"Folder is not picked, {folderPickerResult.Exception.Message}").Show(cancellationToken);
        }
    }
}
