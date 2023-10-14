

using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.CustomizationMenu;

public abstract class CustomizationMenu
{
    private static readonly Overlay.MenuOption GlowingPaintValue = new Overlay.MenuOption("Value", "Float", 30f);
    private static readonly Overlay.MenuOption GlowingPaintToggle = new Overlay.MenuOption("Enable", "Bool", false);
    
    public static void InitiateSubMenu()
    {
        GlowingPaintValue.ValueChangedHandler += GlowingPaintValueChanged;
        GlowingPaintToggle.ValueChangedHandler += GlowingPaintToggled;
    }

    // Event handlers
    private static void GlowingPaintValueChanged(object s, EventArgs e)
    {
        var CustomPage = Tabs.Self_Vehicle.DropDownTabs.CustomizationPage._CustomizationPage;

        CustomPage.Dispatcher.Invoke(delegate 
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > CustomPage.GlowingPaintSlider.Maximum) 
                GlowingPaintValue.Value = (float)CustomPage.GlowingPaintSlider.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < CustomPage.GlowingPaintSlider.Minimum)
                GlowingPaintValue.Value = (float)CustomPage.GlowingPaintSlider.Minimum;
            else
                CustomPage.GlowingPaintSlider.Value = (float)Math.Round((float)s.GetType().GetProperty("Value").GetValue(s), 1);
        });
    }

    private static void GlowingPaintToggled(object s, EventArgs e)
    {
        var CustomPage = Tabs.Self_Vehicle.DropDownTabs.CustomizationPage._CustomizationPage;

        CustomPage.Dispatcher.Invoke(() =>
        {
            CustomPage.GlowingPaintSwitch.IsOn = (bool)GlowingPaintToggle.Value;
        });
    }

    // Menu list for this section
    public static List<Overlay.MenuOption> CustomizationOptions = new List<Overlay.MenuOption>()
    {
        new ("Glowing Paint", "SubHeader"),
        GlowingPaintValue,
        GlowingPaintToggle
    };
}