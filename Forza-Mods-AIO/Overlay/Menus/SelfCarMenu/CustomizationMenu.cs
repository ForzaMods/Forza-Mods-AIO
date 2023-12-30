using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.CustomizationPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu;

public abstract class CustomizationMenu
{
    private static readonly FloatOption GlowingPaintValue = new(
        "Value", 
        50f, 
        Customization.GlowingPaintSlider.Minimum, 
        Customization.GlowingPaintSlider.Maximum, 
        "Glowing paint toggle, you must change the color in the in-game paint car section for it to take effect"
    );
    
    private static readonly ToggleOption GlowingPaintToggle = new("Enable", false);
    
    
    private static readonly FloatOption HeadlightColourRedValue = new("Red", 1, 0, 1);
    private static readonly FloatOption HeadlightColourGreenValue = new("Green", 1, 0, 1);
    private static readonly FloatOption HeadlightColourBlueValue = new("Blue", 1, 0, 1);
    private static readonly ToggleOption HeadlightColourToggle = new("Enable", false, "This feature changes the color your headlights project onto the road");
    
    public static void InitiateSubMenu()
    {
        GlowingPaintValue.ValueChanged += GlowingPaintValueChanged;
        GlowingPaintToggle.Toggled += GlowingPaintToggled;
        
        HeadlightColourRedValue.ValueChanged += HeadlightColourValueChanged;
        HeadlightColourGreenValue.ValueChanged += HeadlightColourValueChanged;
        HeadlightColourBlueValue.ValueChanged += HeadlightColourValueChanged;
        HeadlightColourToggle.Toggled += HeadlightColourToggled;
    }

    private static void GlowingPaintValueChanged(object s, EventArgs e)
    {
        Customization.GlowingPaintSlider.Value = Convert.ToSingle(Math.Round(GlowingPaintValue.Value, 1));
    }

    private static void GlowingPaintToggled(object s, EventArgs e)
    {
        Customization.GlowingPaintSwitch.IsOn = GlowingPaintToggle.IsOn;
    }

    private static void HeadlightColourValueChanged(object s, EventArgs e)
    {
        if (s is not FloatOption floatOption)
        {
            return;
        }

        switch (floatOption.Name)
        {
            case "Red":
            {
                Customization.HeadlightRed.Value = floatOption.Value;
                break;
            }
            case "Green":
            {
                Customization.HeadlightGreen.Value = floatOption.Value;
                break;
            }
            case "Blue":
            {
                Customization.HeadlightBlue.Value = floatOption.Value;
                break;
            }
        }
    }

    private static void HeadlightColourToggled(object s, EventArgs e)
    {
        Customization.HeadlightToggle.IsOn = HeadlightColourToggle.IsOn;
    }

    
    
    public static readonly List<MenuOption> CustomizationOptions = new()
    {
        new SubHeaderOption("Glowing Paint"),
        GlowingPaintValue,
        GlowingPaintToggle,
        new SubHeaderOption("Headlight colour"),
        HeadlightColourRedValue,
        HeadlightColourGreenValue,
        HeadlightColourBlueValue,
        HeadlightColourToggle,
    };
}