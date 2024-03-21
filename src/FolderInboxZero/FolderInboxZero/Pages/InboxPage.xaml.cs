using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using FolderInboxZero.Pages.Base;
using FolderInboxZero.ViewModels;
using System.Threading;

namespace FolderInboxZero.Pages;

public partial class InboxPage: BasePage<InboxViewModel>
{
    public InboxPage(InboxViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
    }    
}