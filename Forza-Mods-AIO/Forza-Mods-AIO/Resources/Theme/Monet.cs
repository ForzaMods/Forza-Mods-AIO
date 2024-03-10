using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using ControlzEx.Theming;
using static System.Windows.Media.ColorConverter;

namespace Forza_Mods_AIO.Resources.Theme;

public sealed class Monet : INotifyPropertyChanged
{
    private static Monet _instance = null!;
    public static Monet GetInstance()
    {
        if (_instance != null!) return _instance;
        _instance = new Monet();
        return _instance;
    }
    
    private Brush _mainColour = new SolidColorBrush((Color)ConvertFromString("#4C566A"));
    public Brush MainColour
    {
        get => _mainColour;
        private set => SetField(ref _mainColour, value);
    }
    
    private Brush _darkishColour = new SolidColorBrush((Color)ConvertFromString("#434C5E"));
    public Brush DarkishColour
    {
        get => _darkishColour;
        private set => SetField(ref _darkishColour, value);
    }
    
    private Brush _darkColour = new SolidColorBrush((Color)ConvertFromString("#3B4252"));
    public Brush DarkColour
    {
        get => _darkColour;
        private set => SetField(ref _darkColour, value);
    }
    
    private Brush _darkerColour = new SolidColorBrush((Color)ConvertFromString("#2E3440"));
    public Brush DarkerColour
    {
        get => _darkerColour;
        private set => SetField(ref _darkerColour, value);
    }

    public Color MainColourAsColour { get; private set; } = (Color)ConvertFromString("#2E3440");

    public void ChangeColor(Color color = default)
    {
        if (color != default)
        {
            // TODO: Implement
        }
        
        MainColour = new SolidColorBrush((Color)ConvertFromString("#FFFFFF"));
        DarkishColour = new SolidColorBrush((Color)ConvertFromString("#EEEEEE"));
        DarkColour = new SolidColorBrush((Color)ConvertFromString("#DDDDDD"));
        DarkerColour = new SolidColorBrush((Color)ConvertFromString("#CCCCCC"));
        
        var converted = (Color)ColorConverter.ConvertFromString("#DDDDDD");
        const string name = "dsrgdfgsdfgdgdrhdrtfhdf";
        ThemeManager.Current.AddTheme(new ControlzEx.Theming.Theme(name,
            name,
            "Dark",
            "Red",
            converted,
            new SolidColorBrush(converted),
            false,
            false));
        ThemeManager.Current.ChangeTheme(Application.Current, name);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}