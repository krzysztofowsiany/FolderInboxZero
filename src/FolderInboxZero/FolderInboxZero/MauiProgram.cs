using Microsoft.Extensions.Logging;
using FolderInboxZero.Core;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using FolderInboxZero.ViewModels;
using FolderInboxZero.ViewModels.Base;
using FolderInboxZero.Pages.Base;
using FolderInboxZero.Pages;


namespace FolderInboxZero;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransientWithShellRoute<MainPage, MainPageViewModel>();
        builder.Services.AddTransientWithShellRoute<SettingsPage, SettingsViewModel>();
        builder.Services.AddTransientWithShellRoute<InboxPage, InboxViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);
        builder.Services.RegisterCore();
        return builder.Build();
    }


    static IServiceCollection AddTransientWithShellRoute<TPage, TViewModel>(this IServiceCollection services) where TPage : BasePage<TViewModel>
                                                                                                                where TViewModel : BaseViewModel
    {
        return services.AddTransientWithShellRoute<TPage, TViewModel>(AppShell.GetPageRoute<TViewModel>());
    }
}
