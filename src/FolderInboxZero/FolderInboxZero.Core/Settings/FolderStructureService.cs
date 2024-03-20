

namespace FolderInboxZero.Core.Settings;

public class FolderStructureService
{
    private readonly SettingsRepository _settingsRepository;
    private IEnumerable<IGrouping<Guid, FolderStructureTable>> _folderStructure;

    public FolderStructureService(SettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    public IGrouping<Guid, FolderStructureTable> GetBaseFolders() => GetSubFoldersForParentId(Guid.Empty);

    public IGrouping<Guid, FolderStructureTable> GetSubFoldersForParentId(Guid parentId)
    {
        var folders = _folderStructure.FirstOrDefault(x => x.Key.Equals(parentId));
        return folders;
    }

    public void LoadStructure()
    {
        _folderStructure = _settingsRepository.LoadStructure();
    }
}
