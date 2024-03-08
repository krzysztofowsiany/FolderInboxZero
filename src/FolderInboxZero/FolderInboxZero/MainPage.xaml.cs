using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using FolderInboxZero.Core.CurrentStorage;
using FolderInboxZero.ViewModels;

namespace FolderInboxZero;

public partial class MainPage : BasePage<FolderPickerViewModel>
{
    private readonly CurrentStorageRepository _currentStorageRepository;
    private readonly IFolderPicker _folderPicker;

    public MainPage(FolderPickerViewModel viewModel) : base(viewModel)
    {
    }

    async Task PickFolder(CancellationToken cancellationToken)
    {
        var result = await _folderPicker.PickAsync(cancellationToken);
        result.EnsureSuccess();
        await Toast.Make($"Folder picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
    }
}
