using FolderInboxZero.Pages.Base;
using FolderInboxZero.ViewModels;

namespace FolderInboxZero.Pages;

public partial class SettingsPage: BasePage<SettingsViewModel>
{
    public SettingsPage(SettingsViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}