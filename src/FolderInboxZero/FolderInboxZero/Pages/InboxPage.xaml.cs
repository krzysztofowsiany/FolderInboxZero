using FolderInboxZero.Pages.Base;
using FolderInboxZero.ViewModels;

namespace FolderInboxZero.Pages;

public partial class InboxPage: BasePage<InboxViewModel>
{
    public InboxPage(InboxViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}