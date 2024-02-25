using System.Windows.Data;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Converters;

public class InstanceEqualsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        return ((Type)parameter!).IsInstanceOfType(value);
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (!(bool)value!)
        {
            return Binding.DoNothing;
        }
        
        var targetTypeAsType = (Type)parameter!;
        return Pages.GetPage(targetTypeAsType);
    }
}