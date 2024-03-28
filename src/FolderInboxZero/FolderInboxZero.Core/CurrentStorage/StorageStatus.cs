using System.ComponentModel.DataAnnotations;

namespace FolderInboxZero.Core.CurrentStorage;

public enum StorageStatus
{
    [Display(Name = "To Do")]
    ToDo = 0,
    [Display(Name = "In Progress")]
    InProgress = 1,
    [Display(Name = "To Delete")]
    ToDelete = 2,
    [Display(Name = "Finished")]
    Finished = 3,
}