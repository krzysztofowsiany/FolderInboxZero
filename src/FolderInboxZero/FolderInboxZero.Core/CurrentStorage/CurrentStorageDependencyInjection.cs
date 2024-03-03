namespace FolderInboxZero.Core.CurrentStorage;

static public class CurrentStorageDependencyInjection
{
    static public IServiceCollection RegisterCurrentStorage(this IServiceCollection services) => services
        .AddSingleton<CurrentStorageRepository>();
}