using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.CustomizationPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu;

public abstract class CustomizationMenu
{
    private static readonly FloatOption GlowingPaintValue = new("Value", 30f, Customization.GlowingPaintSlider.Minimum, Customization.GlowingPaintSlider.Maximum);
    private static readonly ToggleOption GlowingPaintToggle = new("Enable", false);
    
    public static void InitiateSubMenu()
    {
        GlowingPaintValue.ValueChangedEventHandler += GlowingPaintValueChanged;
        GlowingPaintToggle.ToggledEventHandler += GlowingPaintToggled;
    }

    private static void GlowingPaintValueChanged(object s, EventArgs e)
    {
        Customization.GlowingPaintSlider.Value = Convert.ToSingle(Math.Round(GlowingPaintValue.Value, 1));
    }

    private static void GlowingPaintToggled(object s, EventArgs e)
    {
        Customization.GlowingPaintSwitch.IsOn = GlowingPaintToggle.IsOn;
    }

    public static readonly List<MenuOption> CustomizationOptions = new()
    {
        new SubHeaderOption("Glowing Paint"),
        GlowingPaintValue,
        GlowingPaintToggle
    };
}