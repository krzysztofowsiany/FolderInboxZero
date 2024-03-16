using FolderInboxZero.Core.CurrentStorage;
using FolderInboxZero.Core.Settings;

namespace FolderInboxZero.Core;

static public class CoreDependencyInjection
{
    static public IServiceCollection RegisterCore(this IServiceCollection services) => services
        .RegisterSttings()
        .RegisterCurrentStorage();
}
