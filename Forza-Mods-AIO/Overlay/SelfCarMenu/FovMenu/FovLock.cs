using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovLock
{
    private static readonly Overlay.MenuOption FovLockValue = new("Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FovLockToggle = new("Enable", Overlay.MenuOption.OptionType.Bool, false);
        
    public static void InitiateSubMenu()
    {
        FovLockValue.ValueChangedHandler += FovLockValueChanged;
        FovLockToggle.ValueChangedHandler += FovLockToggled;
    }
    
    // Event handlers
    private static void FovLockValueChanged(object s, EventArgs e)
    {
        var FovPage = Tabs.Self_Vehicle.DropDownTabs.FovPage._fovPage;
    
        FovPage.Dispatcher.Invoke(delegate 
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > FovPage.FovSlider.Maximum) 
                FovLockValue.Value = (float)FovPage.FovSlider.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < FovPage.FovSlider.Minimum)
                FovLockValue.Value = (float)FovPage.FovSlider.Minimum;
            else
                FovPage.FovSlider.Value = (float)Math.Round((float)s.GetType().GetProperty("Value").GetValue(s), 1);
        });
    }
    
    private static void FovLockToggled(object s, EventArgs e)
    {
        var FovPage = Tabs.Self_Vehicle.DropDownTabs.FovPage._fovPage;
    
        FovPage.Dispatcher.Invoke(() =>
        {
            FovPage.FovSwitch.IsOn = (bool)FovLockToggle.Value;
        });
    }
    
    // Menu list for this section
    public static List<Overlay.MenuOption> FovLockOptions = new()
    {
        new ("Fov Lock", Overlay.MenuOption.OptionType.SubHeader),
        FovLockValue,
        FovLockToggle
    };
}