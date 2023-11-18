using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ColorThiefDotNet;
using ControlzEx.Theming;
using Forza_Mods_AIO.Tabs.AIO_Info;
using MahApps.Metro.Controls;
using Brush = System.Windows.Media.Brush;
using ColorConverter = System.Windows.Media.ColorConverter;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
using static Forza_Mods_AIO.Resources.DllImports;

namespace Forza_Mods_AIO.Resources.Theme;

internal abstract class Monet
{
    #region Variables
    
    public static Brush? MainColour = new SolidColorBrush((MColor)ColorConverter.ConvertFromString("#4C566A"));
    public static Brush? DarkishColour = new SolidColorBrush((MColor)ColorConverter.ConvertFromString("#434C5E"));
    public static Brush? DarkColour = new SolidColorBrush((MColor)ColorConverter.ConvertFromString("#3B4252"));
    public static Brush? DarkerColour = new SolidColorBrush((MColor)ColorConverter.ConvertFromString("#2E3440"));
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
        int min = Math.Min(color.R, Math.Min(color.G, color.B));

        hue = color.GetHue();
        saturation = max == 0 ? 0 : 1d - 1d * min / max;
        value = max / 255d;
    }
    public static void ApplyMonet()
    {
        var window = IntPtr.Zero;
        EnumWindows((hwnd, _) =>
        {
            var parentHandle = hwnd;
            EnumChildWindows(hwnd, (hwnd, _) =>
            {
                var sb = new StringBuilder(GetWindowTextLength(hwnd) + 1);
                GetWindowText(hwnd, sb, sb.Capacity);
                if (!sb.ToString().Contains("WPE"))
                {
                    return true;
                }

                window = parentHandle;
                return true;
            }, 0);
            return true;
        }, 0);

        var colorThief = new ColorThief();
        if (window == IntPtr.Zero)
        {
            window = GetShellWindow();
        }

        var desktopWallpaper = CaptureWindow(window);
        var colour = colorThief.GetColor(desktopWallpaper).Color;
        desktopWallpaper.Dispose();

        MainWindow.Mw.Dispatcher.Invoke(() =>
        {
            ColorToHsv(ColorTranslator.FromHtml(colour.ToHexString()), out var h, out var s, out var v1);
            v1 *= AioInfo.Ai.LightnessSlider.Value / AioInfo.Ai.LightnessSlider.Maximum;
            if (v1 < 0)
            {
                v1 = 0;
            }

            var v2 = v1 * AioInfo.Ai.LightnessSlider.Value / 1.25 / AioInfo.Ai.LightnessSlider.Maximum;
            if (v2 < 0)
            {
                v2 = 0;
            }

            var v3 = v1 * AioInfo.Ai.LightnessSlider.Value / 1.5 / AioInfo.Ai.LightnessSlider.Maximum;
            if (v3 < 0)
            {
                v3 = 0;
            }

            var v4 = v1 * AioInfo.Ai.LightnessSlider.Value / 2 / AioInfo.Ai.LightnessSlider.Maximum;
            if (v4 < 0)
            {
                v4 = 0;
            }

            var colourHex1 = ColorTranslator.ToHtml(ColorFromHsv(h, s, v1));
            var colourHex2 = ColorTranslator.ToHtml(ColorFromHsv(h, s, v2));
            var colourHex3 = ColorTranslator.ToHtml(ColorFromHsv(h, s, v3));
            var colourHex4 = ColorTranslator.ToHtml(ColorFromHsv(h, s, v4));

            var converter = new BrushConverter();
            MainColour = (Brush)converter.ConvertFromString(colourHex1)!;

            if (MainWindow.Mw.Background.Background.ToString() == MainColour.ToString())
            {
                return;
            }

            DarkishColour = (Brush)converter.ConvertFromString(colourHex2)!;
            DarkColour = (Brush)converter.ConvertFromString(colourHex3)!;
            DarkerColour = (Brush)converter.ConvertFromString(colourHex4)!;

            MainWindow.Mw.Background.Background = MainColour;
            MainWindow.Mw.FrameBorder.Background = MainColour;
            MainWindow.Mw.SideBar.Background = DarkishColour;
            MainWindow.Mw.TopBar1.Background = DarkColour;
            MainWindow.Mw.TopBar2.Background = DarkColour;

            var randName = Guid.NewGuid().ToString();
            ThemeManager.Current.ClearThemes();
            ThemeManager.Current.AddTheme(new(randName, randName, "Dark", "Red", (MColor)ColorConverter.ConvertFromString(colourHex4), DarkerColour, true, false));
            ThemeManager.Current.ChangeTheme(Application.Current, randName);

            MainWindow.Mw.CategoryButton_Click(new object(), new RoutedEventArgs());
            foreach (var visual in MainWindow.Mw.Window.GetChildren())
            {
                var element = (FrameworkElement)visual;
                var elementType = element.GetType();
                if (elementType == typeof(Button))
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, DarkerColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                }
                else if (elementType == typeof(Border))
                {
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                }
                else if (elementType == typeof(NumericUpDown))
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, DarkerColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                }
                else if (elementType == typeof(ComboBox))
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, DarkerColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                }
                else if (elementType == typeof(ListBox))
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, DarkerColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                }
                else if (elementType == typeof(ListBoxItem))
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, MainColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, MainColour);
                }
                else if (elementType == typeof(ComboBoxItem))
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, MainColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, MainColour);
                }
                else if (element.GetType() == typeof(MetroProgressBar))
                {
                    element.GetType().GetProperty("Background")?.SetValue(element, DarkerColour);
                    element.GetType().GetProperty("BorderBrush")?.SetValue(element, DarkerColour);
                }
            }
            
            
        });
    }
    
    
    #endregion
}