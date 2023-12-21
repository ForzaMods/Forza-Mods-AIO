using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovLock
{
    private static readonly MenuOption FovLockValue = new("Value", 0f);
    private static readonly MenuOption FovLockToggle = new("Enable",false);
        
    public static void InitiateSubMenu()
    {
        FovLockValue.ValueChangedHandler += FovLockValueChanged;
        FovLockToggle.ValueChangedHandler += FovLockToggled;
    }
    
    // Event handlers
    private static void FovLockValueChanged(object s, EventArgs e)
    {
        var fovPage = Tabs.Self_Vehicle.DropDownTabs.FovPage.Fov;
    
        fovPage.Dispatcher.Invoke(delegate 
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > fovPage.FovSlider.Maximum) 
                FovLockValue.Value = (float)fovPage.FovSlider.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < fovPage.FovSlider.Minimum)
                FovLockValue.Value = (float)fovPage.FovSlider.Minimum;
            else
                fovPage.FovSlider.Value = (float)Math.Round((float)s.GetType().GetProperty("Value").GetValue(s), 1);
        });
    }
    
    private static void FovLockToggled(object s, EventArgs e)
    {
        var fovPage = Tabs.Self_Vehicle.DropDownTabs.FovPage.Fov;
    
        fovPage.Dispatcher.Invoke(() =>
        {
            fovPage.FovSwitch.IsOn = (bool)FovLockToggle.Value;
        });
    }
    
    // Menu list for this section
    public static List<MenuOption> FovLockOptions = new()
    {
        new ("Fov Lock", OptionType.SubHeader),
        FovLockValue,
        FovLockToggle
    };
}