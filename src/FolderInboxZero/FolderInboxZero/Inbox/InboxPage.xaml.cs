using FolderInboxZero.Pages.Base;

namespace FolderInboxZero.Inbox;

public partial class InboxPage: BasePage<InboxViewModel>
{
    public InboxPage(InboxViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
    }    
}