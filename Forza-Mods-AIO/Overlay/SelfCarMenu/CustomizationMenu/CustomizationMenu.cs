using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.CustomizationMenu;

public abstract class CustomizationMenu
{
    private static readonly MenuOption GlowingPaintValue = new("Value", OptionType.Float, 30f);
    private static readonly MenuOption GlowingPaintToggle = new("Enable", OptionType.Bool, false);
    
    public static void InitiateSubMenu()
    {
        GlowingPaintValue.ValueChangedHandler += GlowingPaintValueChanged;
        GlowingPaintToggle.ValueChangedHandler += GlowingPaintToggled;
    }

    // Event handlers
    private static void GlowingPaintValueChanged(object s, EventArgs e)
    {
        var customPage = Tabs.Self_Vehicle.DropDownTabs.CustomizationPage._CustomizationPage;

        customPage.Dispatcher.Invoke(delegate 
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > customPage.GlowingPaintSlider.Maximum) 
                GlowingPaintValue.Value = (float)customPage.GlowingPaintSlider.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < customPage.GlowingPaintSlider.Minimum)
                GlowingPaintValue.Value = (float)customPage.GlowingPaintSlider.Minimum;
            else
                customPage.GlowingPaintSlider.Value = (float)Math.Round((float)s.GetType().GetProperty("Value").GetValue(s), 1);
        });
    }

    private static void GlowingPaintToggled(object s, EventArgs e)
    {
        var customPage = Tabs.Self_Vehicle.DropDownTabs.CustomizationPage._CustomizationPage;

        customPage.Dispatcher.Invoke(() =>
        {
            customPage.GlowingPaintSwitch.IsOn = (bool)GlowingPaintToggle.Value;
        });
    }

    // Menu list for this section
    public static List<MenuOption> CustomizationOptions = new()
    {
        new ("Glowing Paint", OptionType.SubHeader),
        GlowingPaintValue,
        GlowingPaintToggle
    };
}