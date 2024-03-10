using FolderInboxZero.Pages;
using FolderInboxZero.Pages.Base;
using FolderInboxZero.ViewModels;
using FolderInboxZero.ViewModels.Base;

namespace FolderInboxZero;

public partial class AppShell : Shell
{
    static readonly IReadOnlyDictionary<Type, Type> viewModelMappings = new Dictionary<Type, Type>(
    [
        CreateViewModelMapping<MainPage, MainPageViewModel>(),
    ]);

    public AppShell() => InitializeComponent();

    public static string GetPageRoute<TViewModel>() where TViewModel : BaseViewModel
    {
        return GetPageRoute(typeof(TViewModel));
    }

    public static string GetPageRoute(Type viewModelType)
    {
        if (!viewModelType.IsAssignableTo(typeof(BaseViewModel)))
        {
            throw new ArgumentException($"{nameof(viewModelType)} must implement {nameof(BaseViewModel)}", nameof(viewModelType));
        }

        if (!viewModelMappings.TryGetValue(viewModelType, out Type mapping))
        {
            throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings. Please register your ViewModel in {nameof(AppShell)}.{nameof(viewModelMappings)}");
        }

        var uri = new UriBuilder("", $"//{mapping.Name}");
        return uri.Uri.OriginalString[..^1];
    }

    static KeyValuePair<Type, Type> CreateViewModelMapping<TPage, TViewModel>()
        where TPage : BasePage<TViewModel>
        where TViewModel : BaseViewModel
    {
        return new KeyValuePair<Type, Type>(typeof(TViewModel), typeof(TPage));
    }
}