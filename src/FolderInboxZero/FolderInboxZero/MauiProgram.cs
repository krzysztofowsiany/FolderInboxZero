using Microsoft.Extensions.Logging;
using FolderInboxZero.Core;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;


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

        builder.Services.AddTransientWithShellRoute<MainPage, ViewModels.FolderPickerViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);
        builder.Services.RegisterCore();
        return builder.Build();
    }
}
