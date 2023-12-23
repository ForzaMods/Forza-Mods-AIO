using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using Forza_Mods_AIO.Overlay.Options;
using static System.Convert;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Menus.SettingsMenu;

public class SettingsMenu
{
    private static readonly IntOption HeaderImage = new("Header", 1);

    private static readonly IntOption XOffset = IntOption.CreateWithMinimum("X Offset", 0,0);
    private static readonly IntOption YOffset = IntOption.CreateWithMinimum("Y Offset", 0,0);
        
    #region Background options

    public static readonly IntOption MainBackgroundR = new("Background R", 0,0, 255);
    public static readonly IntOption MainBackgroundG = new("Background G", 0,0, 255);
    public static readonly IntOption MainBackgroundB = new("Background B", 0,0, 255);
    public static readonly IntOption MainBackgroundA = new("Background Alpha", 120,0, 255);

    public static readonly IntOption DescriptionBackgroundR = new("Background R", 0,0, 255);
    public static readonly IntOption DescriptionBackgroundG = new("Background G", 0,0, 255);
    public static readonly IntOption DescriptionBackgroundB = new("Background B", 0,0, 255);
    public static readonly IntOption DescriptionBackgroundA = new("Background Alpha", 120,0, 255);

    #endregion

    #region Border options

    public static readonly IntOption MainBorderR = new("Border R", 0,0, 255);
    public static readonly IntOption MainBorderG = new("Border G", 0,0, 255);
    public static readonly IntOption MainBorderB = new("Border B", 0,0, 255);
    public static readonly IntOption MainBorderA = new("Border Alpha", 255,0, 255);

    public static readonly IntOption DescriptionBorderR = new("Border R", 0,0, 255);
    public static readonly IntOption DescriptionBorderG = new("Border G", 0,0, 255);
    public static readonly IntOption DescriptionBorderB = new("Border B", 0,0, 255);
    public static readonly IntOption DescriptionBorderA = new("Border Alpha", 255,0, 255);


    #endregion

        
    private static readonly string[] FontStyles =
    {
        "Normal",
        "Italic",
        "Oblique"
    };
    
    private static readonly string[] FontWeighs =
    {
        "Thin",
        "Normal",
        "Semi Bold",
        "Bold",
        "Extra Bold",
    };
    
    public static readonly FloatOption FontSize = new("Size", 5f, 1f, 5f);
    public static readonly SelectionOption FontWeight = new("Weight", 1, FontWeighs);
    public static readonly SelectionOption FontStyle = new("Style", 0, FontStyles);
    
    #region Eventhandlers and similar stuff
    public void InitiateSubMenu()
    {
        HeaderImage.ValueChanged += HeaderValueChanged;

        MainBackgroundR.ValueChanged += ColourValueChanged;
        MainBackgroundG.ValueChanged += ColourValueChanged;
        MainBackgroundB.ValueChanged += ColourValueChanged;
        MainBackgroundA.ValueChanged += ColourValueChanged;

        DescriptionBackgroundR.ValueChanged += ColourValueChanged;
        DescriptionBackgroundG.ValueChanged += ColourValueChanged;
        DescriptionBackgroundB.ValueChanged += ColourValueChanged;
        DescriptionBackgroundA.ValueChanged += ColourValueChanged;

        MainBorderR.ValueChanged += ColourValueChanged;
        MainBorderG.ValueChanged += ColourValueChanged;
        MainBorderB.ValueChanged += ColourValueChanged;
        MainBorderA.ValueChanged += ColourValueChanged;

        DescriptionBorderR.ValueChanged += ColourValueChanged;
        DescriptionBorderG.ValueChanged += ColourValueChanged;
        DescriptionBorderB.ValueChanged += ColourValueChanged;
        DescriptionBorderA.ValueChanged += ColourValueChanged;

        FontSize.ValueChanged += FontSizeValueChanged;
        FontWeight.SelectionChanged += FontWeightSelectionChanged;
        FontStyle.SelectionChanged += FontStyleSelectionChanged;

        XOffset.ValueChanged += XOffsetValueChanged;
        YOffset.ValueChanged += YOffsetValueChanged;

        O.Dispatcher.Invoke(() =>
        {
            Oh.MainBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
            Oh.DescriptionBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
        });
    }
        
    // Event handlers
    private static void ColourValueChanged(object s, EventArgs e)
    {
        O.Dispatcher.Invoke(() =>
        {
            if (GetPropertyName(s).Contains("Main"))
            {
                Oh.MainBackColour = new SolidColorBrush(Color.FromArgb(
                    ToByte(MainBackgroundA.Value),
                    ToByte(MainBackgroundR.Value),
                    ToByte(MainBackgroundG.Value),
                    ToByte(MainBackgroundB.Value)));
                Oh.MainBorderColour = new SolidColorBrush(Color.FromArgb(
                    ToByte(MainBorderA.Value),
                    ToByte(MainBorderR.Value),
                    ToByte(MainBorderG.Value),
                    ToByte(MainBorderB.Value)));
            }
            else
            {
                Oh.DescriptionBackColour = new SolidColorBrush(Color.FromArgb(
                    ToByte(DescriptionBackgroundA.Value),
                    ToByte(DescriptionBackgroundR.Value),
                    ToByte(DescriptionBackgroundG.Value),
                    ToByte(DescriptionBackgroundB.Value)));
                Oh.DescriptionBorderColour = new SolidColorBrush(Color.FromArgb(
                    ToByte(DescriptionBorderA.Value),
                    ToByte(DescriptionBorderR.Value),
                    ToByte(DescriptionBorderG.Value),
                    ToByte(DescriptionBorderB.Value)));
            }
        });
    }

    private static string GetPropertyName(object field)
    {
        var fieldInfo = typeof(SettingsMenu).GetFields(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(f => f.GetValue(null) == field);

        return fieldInfo?.Name ?? "Unknown";
    }
    
    private static void HeaderValueChanged(object s, EventArgs e)
    {
        var headerCount = Oh.Headers.Count;

        if (headerCount == 0)
        {
            HeaderImage.ValueChanged -= HeaderValueChanged;
            HeaderImage.Value = 1;
            HeaderImage.ValueChanged += HeaderValueChanged;
            return;
        }

        if ((int)s.GetType().GetProperty("Value")?.GetValue(s)! < 1)
        {
            s.GetType().GetProperty("Value")?.SetValue(s, headerCount);
        }
        else if ((int)s.GetType().GetProperty("Value")?.GetValue(s)! > headerCount)
        {
            s.GetType().GetProperty("Value")?.SetValue(s, 1);
        }

        Oh.HeaderIndex = (int)s.GetType().GetProperty("Value")?.GetValue(s)! - 1;
    }

    private static void XOffsetValueChanged(object s, EventArgs e)
    {
        Oh.XOffset = XOffset.Value;
    }
    
    private static void YOffsetValueChanged(object s, EventArgs e)
    {
        Oh.YOffset = YOffset.Value;
    }

    private static void FontSizeValueChanged(object s, EventArgs e)
    {
        Oh.FontSize = FontSize.Value;
    }
    
    private static void FontStyleSelectionChanged(object s, EventArgs e)
    {
        Oh.FontStyle = FontStyles[FontStyle.Index];
    }
    
    private static void FontWeightSelectionChanged(object s, EventArgs e)
    {
        Oh.FontWeight = FontWeighs[FontWeight.Index];
    }
    
    #endregion
        
    public static readonly List<MenuOption> SettingsOptions = new()
    {
        HeaderImage,
        new ButtonOption("Refresh Headers", Oh.CacheHeaders),
        new MenuButtonOption("Font Settings"),
        new MenuButtonOption("Main area"),
        new MenuButtonOption("Description area"),
        new ButtonOption("Save Settings", Oh.SaveSettings),
        new ButtonOption("Load Settings", Oh.LoadSettings) 
    };

    public static readonly List<MenuOption> MainAreaOptions = new()
    {
        new SubHeaderOption("Background"),
        MainBackgroundR,
        MainBackgroundG,
        MainBackgroundB,
        MainBackgroundA,
        new SubHeaderOption("Border"),
        MainBorderR,
        MainBorderG,
        MainBorderB,
        MainBorderA
    };
    public static readonly List<MenuOption> DescriptionAreaOptions = new()
    {
        new SubHeaderOption("Background"),
        DescriptionBackgroundR,
        DescriptionBackgroundG,
        DescriptionBackgroundB,
        DescriptionBackgroundA,
        new SubHeaderOption("Border"),
        DescriptionBorderR,
        DescriptionBorderG,
        DescriptionBorderB,
        DescriptionBorderA
    };

    public static readonly List<MenuOption> PositionOptions = new()
    {
        new SubHeaderOption("Position"),
        XOffset,
        YOffset
    };

    
    public static readonly List<MenuOption> FontOptions = new()
    {
        new SubHeaderOption("Font Options"),
        FontSize,
        FontWeight,
        FontStyle
    };
}