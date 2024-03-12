using FolderInboxZero.Pages.Base;

namespace FolderInboxZero.Pages;

public partial class SettingsPage BasePage<SettingsViewModel>
{
    public SettingsPage(SettingsViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}