using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using FolderInboxZero.Core.CurrentStorage;
using FolderInboxZero.ViewModels.Base;

namespace FolderInboxZero.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    readonly IFolderPicker _folderPicker;
    private readonly CurrentStorageRepository _currentStorageRepository;

    public MainPageViewModel(IFolderPicker folderPicker, CurrentStorageRepository currentStorageRepository)
    {
        _folderPicker = folderPicker;

        _currentStorageRepository = currentStorageRepository;
    }

    [RelayCommand]
    async Task PickFolder(CancellationToken cancellationToken)
    {
        var folderPickerResult = await _folderPicker.PickAsync(cancellationToken);
        if (!folderPickerResult.IsSuccessful) return;

        _currentStorageRepository.SetCurrentDirectory(folderPickerResult.Folder.Path);
        _currentStorageRepository.Connect();

        var currentStorageTableBuilder = new CurrentStorageTableStructureBuilder();
        currentStorageTableBuilder.GetStorageItems(folderPickerResult.Folder.Path);

        _currentStorageRepository.AddStorages(currentStorageTableBuilder.StorageItems);

        await Toast.Make($"Folder picked: Name - {folderPickerResult.Folder.Name}, Path - {folderPickerResult.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        return;
    }

    [RelayCommand]
    async Task Settings(CancellationToken cancellationToken)
    {
        return;
    }
}