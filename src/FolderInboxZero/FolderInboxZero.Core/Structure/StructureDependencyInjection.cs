namespace FolderInboxZero.Core.Structure;

static public class StructureDependencyInjection
{
    static public IServiceCollection RegisterStructure(this IServiceCollection services) => services
        .AddSingleton<StructureRepository>();
}
