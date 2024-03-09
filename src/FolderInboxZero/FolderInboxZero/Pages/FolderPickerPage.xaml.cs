using FolderInboxZero.Pages.Base;
using FolderInboxZero.ViewModels;

namespace FolderInboxZero.Pages;

public partial class FolderPickerPage : BasePage<FolderPickerViewModel>
{
	public FolderPickerPage(FolderPickerViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}