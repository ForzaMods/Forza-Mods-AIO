using System.Globalization;
using System.Windows.Data;

namespace Forza_Mods_AIO.Converters;

public class IntParamConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var index = (int)values[0];
        return index + 1 <= values.Length - 1 ? values[index + 1] : new object();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}