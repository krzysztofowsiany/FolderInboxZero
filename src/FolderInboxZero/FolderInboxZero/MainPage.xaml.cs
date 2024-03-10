using FolderInboxZero.Pages.Base;
using FolderInboxZero.ViewModels;

namespace FolderInboxZero;

public partial class MainPage : BasePage<MainPageViewModel>
{
    public MainPage(MainPageViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
