using System;
using System.Collections.Generic;
using System.Windows.Media;
using static System.Convert;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SettingsMenu;

public class SettingsMenu
{
    // Header Option
    private static readonly MenuOption HeaderImage = new("Header", OptionType.Int, 1);

    public static readonly MenuOption XOffset = new("X Offset", OptionType.Int, 0, min: 0);
    public static readonly MenuOption YOffset = new("Y Offset", OptionType.Int, 0, min: 0);
        
    #region Background options

    private static readonly MenuOption MainBackgroundR = new("Background R", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption MainBackgroundG = new("Background G", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption MainBackgroundB = new("Background B", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption MainBackgroundA = new("Background Alpha", OptionType.Int, 120, min: 0, max: 255);

    private static readonly MenuOption DescriptionBackgroundR = new("Background R", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption DescriptionBackgroundG = new("Background G", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption DescriptionBackgroundB = new("Background B", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption DescriptionBackgroundA = new("Background Alpha", OptionType.Int, 120, min: 0, max: 255);

    #endregion

    #region Border options

    private static readonly MenuOption MainBorderR = new("Border R", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption MainBorderG = new("Border G", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption MainBorderB = new("Border B", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption MainBorderA = new("Border Alpha", OptionType.Int, 255, min: 0, max: 255);

    private static readonly MenuOption DescriptionBorderR = new("Border R", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption DescriptionBorderG = new("Border G", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption DescriptionBorderB = new("Border B", OptionType.Int, 0, min: 0, max: 255);
    private static readonly MenuOption DescriptionBorderA = new("Border Alpha", OptionType.Int, 255, min: 0, max: 255);
    public static readonly MenuOption LoadSettings = new("Load Settings", OptionType.Button, delegate { Oh.LoadSettings(); }, isEnabled: false);


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
    
    private static readonly MenuOption FontSize = new("Size", OptionType.Float, 5f, min: 1f, max: 5f);
    private static readonly MenuOption FontWeight = new("Weight", OptionType.Selection, 1, FontWeighs);
    private static readonly MenuOption FontStyle = new("Style", OptionType.Selection, 0, FontStyles);
    
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
    void ColourValueChanged(object S, EventArgs E)
    {
        O?.Dispatcher.Invoke(() =>
        {
            if (Oh.CurrentMenu.Contains("MainArea"))
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
        new("Refresh Headers", OptionType.Button, delegate { Oh.CacheHeaders(); }),
        new("Position", OptionType.MenuButton),
        new("Font Settings", OptionType.MenuButton),
        new("Main area", OptionType.MenuButton),
        new("Description area", OptionType.MenuButton),
        new("Save Settings", OptionType.Button, delegate { Oh.SaveSettings(); LoadSettings.IsEnabled = true; }, isEnabled: false),
        LoadSettings
    };

    // Submenu lists
    public static readonly List<MenuOption> MainAreaOptions = new()
    {
        new ("Background", OptionType.SubHeader),
        MainBackgroundR,
        MainBackgroundG,
        MainBackgroundB,
        MainBackgroundA,
        new ("Border", OptionType.SubHeader),
        MainBorderR,
        MainBorderG,
        MainBorderB,
        MainBorderA
    };
    public static readonly List<MenuOption> DescriptionAreaOptions = new()
    {
        new ("Background", OptionType.SubHeader),
        DescriptionBackgroundR,
        DescriptionBackgroundG,
        DescriptionBackgroundB,
        DescriptionBackgroundA,
        new ("Border", OptionType.SubHeader),
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