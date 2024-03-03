using FolderInboxZero.Core.CurrentStorage;
using FolderInboxZero.Core.Structure;

namespace FolderInboxZero.Core;

static public class CoreDependencyInjection
{
    static public IServiceCollection RegisterCore(this IServiceCollection services) => services
        .RegisterStructure()
        .RegisterCurrentStorage();
}
