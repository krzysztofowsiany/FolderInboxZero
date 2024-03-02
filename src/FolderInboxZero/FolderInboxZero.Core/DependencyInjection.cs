using FolderInboxZero.Core.StorageDatabase;

namespace FolderInboxZero.Core;

static public class DependencyInjection
{
    static public IServiceCollection RegisterCore(this IServiceCollection services)
    {
        services.AddSingleton<StorageDatabaseRepository>();
        return services;
    }
}
