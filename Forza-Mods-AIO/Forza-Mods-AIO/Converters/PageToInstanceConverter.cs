using System.Windows.Data;
using System.Globalization;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Converters;

public class PageToInstanceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Pages.GetPage((Type)parameter!);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}