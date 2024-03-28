using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FolderInboxZero.Inbox;

class StorageStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "[" + ((Enum)value).GetAttributeOfType<DisplayAttribute>().Name + "]";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return default;
    }
}