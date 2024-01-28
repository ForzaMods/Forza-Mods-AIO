using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ColorThiefDotNet;
using MahApps.Metro.Controls;
using Brush = System.Windows.Media.Brush;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.AIO_Info.AioInfo;
using static System.Drawing.ColorTranslator;
using static System.Windows.Media.ColorConverter;
using static ControlzEx.Theming.ThemeManager;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Resources.Theme;

internal abstract class Monet
{
    #region Variables
    
    public static Brush MainColour = new SolidColorBrush((MColor)ConvertFromString("#4C566A"));
    public static Brush DarkishColour = new SolidColorBrush((MColor)ConvertFromString("#434C5E"));
    public static Brush DarkColour = new SolidColorBrush((MColor)ConvertFromString("#3B4252"));
    public static Brush DarkerColour = new SolidColorBrush((MColor)ConvertFromString("#2E3440"));
    private static readonly ColorThief ColorThief = new();
    private const uint PwClientOnly = 1, PwRenderFullContent = 2;
    
    #endregion
    #region Functions
    
    private static Bitmap CaptureWindow(IntPtr handle)
    {
        //https://stackoverflow.com/questions/69605967/c-printwindow-api-returns-black-or-partial-images-alternative-methods#comment129026870_69605967
        //fix for some windows showing up as black/blank
        var rect = new Rectangle();
        GetWindowRect(handle, ref rect);

        rect.Width -= rect.X;
        rect.Height -= rect.Y;

        var bitmap = new Bitmap(rect.Width, rect.Height);
        using var g = Graphics.FromImage(bitmap);
        var hdc = g.GetHdc();
        if (!PrintWindow(handle, hdc, PwClientOnly | PwRenderFullContent))
        {
            Marshal.GetLastWin32Error();
        }
        g.ReleaseHdc(hdc);
        return bitmap;
    }
    private static DColor ColorFromHsv(double hue, double saturation, double value)
    {
        var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        var f = hue / 60 - Math.Floor(hue / 60);

        value *= 255;
        var v = Convert.ToInt32(value);
        var p = Convert.ToInt32(value * (1 - saturation));
        var q = Convert.ToInt32(value * (1 - f * saturation));
        var t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => DColor.FromArgb(255, v, t, p),
            1 => DColor.FromArgb(255, q, v, p),
            2 => DColor.FromArgb(255, p, v, t),
            3 => DColor.FromArgb(255, p, q, v),
            4 => DColor.FromArgb(255, t, p, v),
            _ => DColor.FromArgb(255, v, p, q)
        };
    }
    private static void ColorToHsv(DColor color, out double hue, out double saturation, out double value)
    {
        int max = Math.Max(color.R, Math.Max(color.G, color.B));

        hue = color.GetHue();
        saturation = color.GetSaturation();;
        value = max / 255d;
    }
    public static void ApplyMonet()
    {
        var window = IntPtr.Zero;
        EnumWindows((hwnd, _) =>
        {
            var parentHandle = hwnd;
            EnumChildWindows(parentHandle, (hWnd, _) =>
            {
                var sb = new StringBuilder(GetWindowTextLength(hWnd) + 1);
                var windowText = GetWindowText(hWnd, sb, sb.Capacity);
                if (windowText == -1)
                {
                    return false;
                }
                
                if (!sb.ToString().Contains("WPE"))
                {
                    return true;
                }

                window = parentHandle;
                return true;
            }, 0);
            return true;
        }, 0);

        
        if (window == IntPtr.Zero)
        {
            window = GetShellWindow();
        }

        var desktopWallpaper = CaptureWindow(window);
        var colour = ColorThief.GetColor(desktopWallpaper).Color;
        desktopWallpaper.Dispose();
        Mw.Dispatcher.Invoke(() => SetColours(colour));
    }

    private static void SetColours(ColorThiefDotNet.Color colour)
    {
        ColorToHsv(FromHtml(colour.ToHexString()), out var h, out var s, out var v1);

        var value = Ai.LightnessSlider.Value;
        var maximum = Ai.LightnessSlider.Maximum;
            
        v1 *= value / maximum;
        if (v1 < 0)
        {
            v1 = 0;
        }

        var v2 = v1 * value / 1.25 / maximum;
        if (v2 < 0)
        {
            v2 = 0;
        }

        var v3 = v1 * value / 1.5 / maximum;
        if (v3 < 0)
        {
            v3 = 0;
        }

        var v4 = v1 * value / 2 / maximum;
        if (v4 < 0)
        {
            v4 = 0;
        }

        var colourHex1 = ToHtml(ColorFromHsv(h, s, v1));
        var colourHex2 = ToHtml(ColorFromHsv(h, s, v2));
        var colourHex3 = ToHtml(ColorFromHsv(h, s, v3));
        var colourHex4 = ToHtml(ColorFromHsv(h, s, v4));

        var converter = new BrushConverter();
        MainColour = converter.ConvertFromString(colourHex1) as Brush ?? new DrawingBrush();

        if (Mw.BackgroundBorder.Background.ToString() == MainColour.ToString())
        {
            return;
        }

        DarkishColour = (Brush)converter.ConvertFromString(colourHex2)!;
        DarkColour = (Brush)converter.ConvertFromString(colourHex3)!;
        DarkerColour = (Brush)converter.ConvertFromString(colourHex4)!;

        Mw.BackgroundBorder.Background = MainColour;
        Mw.SideBar.Background = DarkishColour;
        Mw.TopBar.Background = DarkColour;

        var randName = Guid.NewGuid().ToString();
        Current.ClearThemes();
        Current.AddTheme(new(randName, randName, "Dark", "Red", (MColor)ConvertFromString(colourHex4), DarkerColour, true, false));
        Current.ChangeTheme(Application.Current, randName);

        Mw.CategoryButton_Click(Mw.AioInfo, new RoutedEventArgs());
        foreach (var visual in Mw.Window.GetChildren())
        {
            var element = (FrameworkElement)visual;
            var elementType = element.GetType();
                
            switch (elementType)
            {
                case not null when elementType == typeof(Border):
                {
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                    break;
                }
                case not null when elementType == typeof(ComboBox) || 
                                   elementType == typeof(ListBox) ||
                                   elementType == typeof(MetroProgressBar) ||
                                   elementType == typeof(NumericUpDown) ||
                                   elementType == typeof(Button) ||
                                   elementType == typeof(TextBox):
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, DarkerColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                    break;
                }

                case not null when elementType == typeof(ListBoxItem) || 
                                   elementType == typeof(ComboBoxItem):
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, MainColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, MainColour);
                    break;
                }
            }
        }
    }
    
    #endregion
}