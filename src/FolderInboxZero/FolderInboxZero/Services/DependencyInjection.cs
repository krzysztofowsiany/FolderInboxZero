namespace FolderInboxZero.Services;

static public class DependencyInjection
{
    static public IServiceCollection RegisterServices(this IServiceCollection services) => services
       .AddScoped<LoadFolderStructureService>();
}
