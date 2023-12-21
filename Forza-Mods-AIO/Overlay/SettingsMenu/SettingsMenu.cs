using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using static System.Convert;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SettingsMenu;

public class SettingsMenu
{
    // Header Option
    private static readonly MenuOption HeaderImage = new("Header", 1);

    private static readonly MenuOption XOffset = new("X Offset", 0, min: 0);
    private static readonly MenuOption YOffset = new("Y Offset", 0, min: 0);
        
    #region Background options

    public static readonly MenuOption MainBackgroundR = new("Background R", 0, min: 0, max: 255);
    public static readonly MenuOption MainBackgroundG = new("Background G", 0, min: 0, max: 255);
    public static readonly MenuOption MainBackgroundB = new("Background B", 0, min: 0, max: 255);
    public static readonly MenuOption MainBackgroundA = new("Background Alpha", 120, min: 0, max: 255);

    public static readonly MenuOption DescriptionBackgroundR = new("Background R", 0, min: 0, max: 255);
    public static readonly MenuOption DescriptionBackgroundG = new("Background G", 0, min: 0, max: 255);
    public static readonly MenuOption DescriptionBackgroundB = new("Background B", 0, min: 0, max: 255);
    public static readonly MenuOption DescriptionBackgroundA = new("Background Alpha", 120, min: 0, max: 255);

    #endregion

    #region Border options

    public static readonly MenuOption MainBorderR = new("Border R", 0, min: 0, max: 255);
    public static readonly MenuOption MainBorderG = new("Border G", 0, min: 0, max: 255);
    public static readonly MenuOption MainBorderB = new("Border B", 0, min: 0, max: 255);
    public static readonly MenuOption MainBorderA = new("Border Alpha", 255, min: 0, max: 255);

    public static readonly MenuOption DescriptionBorderR = new("Border R", 0, min: 0, max: 255);
    public static readonly MenuOption DescriptionBorderG = new("Border G", 0, min: 0, max: 255);
    public static readonly MenuOption DescriptionBorderB = new("Border B", 0, min: 0, max: 255);
    public static readonly MenuOption DescriptionBorderA = new("Border Alpha", 255, min: 0, max: 255);


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
    
    public static readonly MenuOption FontSize = new("Size", 5f, min: 1f, max: 5f);
    public static readonly MenuOption FontWeight = new("Weight", 1, FontWeighs);
    public static readonly MenuOption FontStyle = new("Style", 0, FontStyles);
    
    // Subscribes menu options to event handlers

    #region Eventhandlers and similar stuff
    public void InitiateSubMenu()
    {
        HeaderImage.ValueChangedHandler += HeaderValueChanged;

        MainBackgroundR.ValueChangedHandler += ColourValueChanged;
        MainBackgroundG.ValueChangedHandler += ColourValueChanged;
        MainBackgroundB.ValueChangedHandler += ColourValueChanged;
        MainBackgroundA.ValueChangedHandler += ColourValueChanged;

        DescriptionBackgroundR.ValueChangedHandler += ColourValueChanged;
        DescriptionBackgroundG.ValueChangedHandler += ColourValueChanged;
        DescriptionBackgroundB.ValueChangedHandler += ColourValueChanged;
        DescriptionBackgroundA.ValueChangedHandler += ColourValueChanged;

        MainBorderR.ValueChangedHandler += ColourValueChanged;
        MainBorderG.ValueChangedHandler += ColourValueChanged;
        MainBorderB.ValueChangedHandler += ColourValueChanged;
        MainBorderA.ValueChangedHandler += ColourValueChanged;

        DescriptionBorderR.ValueChangedHandler += ColourValueChanged;
        DescriptionBorderG.ValueChangedHandler += ColourValueChanged;
        DescriptionBorderB.ValueChangedHandler += ColourValueChanged;
        DescriptionBorderA.ValueChangedHandler += ColourValueChanged;

        FontSize.ValueChangedHandler += FontSizeValueChanged;
        FontWeight.ValueChangedHandler += FontWeightSelectionChanged;
        FontStyle.ValueChangedHandler += FontStyleSelectionChanged;

        XOffset.ValueChangedHandler += XOffsetValueChanged;
        YOffset.ValueChangedHandler += YOffsetValueChanged;

        O?.Dispatcher.Invoke(() =>
        {
            Oh.MainBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
            Oh.DescriptionBackColour = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
        });
    }
        
    // Event handlers
    private static void ColourValueChanged(object S, EventArgs E)
    {
        O.Dispatcher.Invoke(() =>
        {
            if (GetPropertyName(S).Contains("Main"))
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
    
    private static void HeaderValueChanged(object S, EventArgs E)
    {
        var HeaderCount = Oh.Headers.Count;

        if (HeaderCount == 0)
        {
            HeaderImage.ValueChangedHandler -= HeaderValueChanged;
            HeaderImage.Value = 1;
            HeaderImage.ValueChangedHandler += HeaderValueChanged;
            return;
        }

        if ((int)S.GetType().GetProperty("Value")?.GetValue(S)! < 1)
        {
            S.GetType().GetProperty("Value")?.SetValue(S, HeaderCount);
        }
        else if ((int)S.GetType().GetProperty("Value")?.GetValue(S)! > HeaderCount)
        {
            S.GetType().GetProperty("Value")?.SetValue(S, 1);
        }

        Oh.HeaderIndex = (int)S.GetType().GetProperty("Value")?.GetValue(S)! - 1;
    }

    private static void XOffsetValueChanged(object S, EventArgs E)
    {
        Oh.XOffset = (int)XOffset.Value;
    }
    
    private static void YOffsetValueChanged(object S, EventArgs E)
    {
        Oh.YOffset = (int)YOffset.Value;
    }

    private static void FontSizeValueChanged(object S, EventArgs E)
    {
        Oh.FontSize = (float)FontSize.Value;
    }
    
    private static void FontStyleSelectionChanged(object S, EventArgs E)
    {
        Oh.FontStyle = FontStyles[(int)FontStyle.Value];
    }
    
    private static void FontWeightSelectionChanged(object S, EventArgs E)
    {
        Oh.FontWeight = FontWeighs[(int)FontWeight.Value];
    }
    
    #endregion
        
    // Menu list for this section
    public static readonly List<MenuOption> SettingsOptions = new()
    {
        HeaderImage,
        new MenuOption("Refresh Headers",  delegate { Oh.CacheHeaders(); }),
        //new MenuOption("Position", OptionType.MenuButton),
        new MenuOption("Font Settings", OptionType.MenuButton),
        new MenuOption("Main area", OptionType.MenuButton),
        new MenuOption("Description area", OptionType.MenuButton),
        new MenuOption("Save Settings",  delegate { Oh.SaveSettings(); }),
        new MenuOption("Load Settings",  delegate { Oh.LoadSettings(); }) 
    };

    // Submenu lists
    public static readonly List<MenuOption> MainAreaOptions = new()
    {
        new MenuOption("Background", OptionType.SubHeader),
        MainBackgroundR,
        MainBackgroundG,
        MainBackgroundB,
        MainBackgroundA,
        new MenuOption("Border", OptionType.SubHeader),
        MainBorderR,
        MainBorderG,
        MainBorderB,
        MainBorderA
    };
    public static readonly List<MenuOption> DescriptionAreaOptions = new()
    {
        new MenuOption("Background", OptionType.SubHeader),
        DescriptionBackgroundR,
        DescriptionBackgroundG,
        DescriptionBackgroundB,
        DescriptionBackgroundA,
        new MenuOption("Border", OptionType.SubHeader),
        DescriptionBorderR,
        DescriptionBorderG,
        DescriptionBorderB,
        DescriptionBorderA
    };

    public static readonly List<MenuOption> PositionOptions = new()
    {
        new MenuOption("Position", OptionType.SubHeader),
        XOffset,
        YOffset
    };

    
    public static readonly List<MenuOption> FontOptions = new()
    {
        new MenuOption("Font Options", OptionType.SubHeader),
        FontSize,
        FontWeight,
        FontStyle
    };
}