namespace FolderInboxZero.Core.Settings;

static public class SettingsDependencyInjection
{
    static public IServiceCollection RegisterSttings(this IServiceCollection services) => services
        .AddSingleton<SettingsRepository>();
}
