using FolderInboxZero.Core.CurrentStorage;

namespace FolderInboxZero;

public partial class MainPage : ContentPage
{
    private readonly CurrentStorageRepository _currentStorageRepository;
    int count = 0;

    public MainPage(CurrentStorageRepository currentStorageRepository)
    {
        InitializeComponent();
        _currentStorageRepository = currentStorageRepository;

        var extractLocalFolderStructureService = new ExtractLocalFolderStructureService();
        var items =  extractLocalFolderStructureService.GetStorageItems(AppDomain.CurrentDomain.BaseDirectory);
        _currentStorageRepository.AddStorages(items);
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
